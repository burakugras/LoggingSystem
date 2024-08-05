using Core.CrossCutingConcerns.Logging.SeriLog.ConfigurationModels;
using Core.CrossCutingConcerns.Logging.SeriLog.CustomColumnEvents;
using Core.CrossCutingConcerns.Logging.SeriLog.CustomColumnOptions;
using Core.CrossCutingConcerns.Logging.SeriLog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace Core.CrossCutingConcerns.Logging.SeriLog.Logger
{
    public class MsSqlLogger : LoggerServiceBase
    {
        public MsSqlLogger(IConfiguration configuration)
        {
            try
            {
                MsSqlConfiguration logConfiguration =
                    configuration.GetSection("SeriLogConfigurations:MsSqlConfiguration").Get<MsSqlConfiguration>()
                    ?? throw new Exception(SerilogMessages.NullOptionsMessage);

                MSSqlServerSinkOptions sinkOptions = new()
                {
                    TableName = logConfiguration.TableName,
                    AutoCreateSqlDatabase = logConfiguration.AutoCreateSqlDatabase,
                    AutoCreateSqlTable = logConfiguration.AutoCreateSqlTable
                };

                var usernameColumnOptions = CustomUsernameColumnOptions.CreateUsernameColumnOptions();
                var userIdColumnOptions = CustomUserIdColumnOptions.CreateUserIdColumnOptions();
                var methodNameColumnOptions = CustomMethodNameColumnOptions.CreateMethodNameColumnOptions();

                var columnOptions = new ColumnOptions
                {
                    Id = { ColumnName = "Id", DataType = SqlDbType.Int, AllowNull = false, DataLength = 128 },
                    Store = new Collection<StandardColumn>
                    {
                        StandardColumn.Id,
                        StandardColumn.TimeStamp,
                        StandardColumn.Message
                    },
                    TimeStamp = { ColumnName = "Date", DataType = SqlDbType.DateTimeOffset, AllowNull = false },
                    Message = { ColumnName = "Description", DataType = SqlDbType.NVarChar, AllowNull = true, DataLength = 4000 },
                    AdditionalColumns = usernameColumnOptions.AdditionalColumns
                                         .Concat(userIdColumnOptions.AdditionalColumns)
                                         .Concat(methodNameColumnOptions.AdditionalColumns)
                                         .ToList()
                };

                columnOptions.Store.Remove(StandardColumn.Level);
                columnOptions.Store.Remove(StandardColumn.Exception);
                columnOptions.Store.Remove(StandardColumn.Properties);
                columnOptions.Store.Remove(StandardColumn.MessageTemplate);

                Serilog.Core.Logger seriLogConfig = new LoggerConfiguration().WriteTo
                    .MSSqlServer(
                        logConfiguration.ConnectionString,
                        sinkOptions,
                        columnOptions: columnOptions)
                        .Enrich.FromLogContext()
                        .Enrich.With<CustomUsernameColumnEvent>()
                        .Enrich.With<CustomUserIdColumnEvent>()
                        .Enrich.With<CustomMethodNameColumnEvent>()
                        .MinimumLevel.Information()
                        .CreateLogger();

                Logger = seriLogConfig;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing MsSqlLogger: {ex.Message}");
                throw;
            }
        }
    }
}
