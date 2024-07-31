using Core.CrossCutingConcerns.Logging.SeriLog.ConfigurationModels;
using Core.CrossCutingConcerns.Logging.SeriLog.CustomColumnEvents;
using Core.CrossCutingConcerns.Logging.SeriLog.CustomColumnOptions;
using Core.CrossCutingConcerns.Logging.SeriLog.Messages;
using Core.CrossCutingConcerns.Logging;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Data;
using Microsoft.Extensions.Configuration;

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
            var methodNameColumnOptions = CustomMethodNameColumnOptions.CreateMethodNameColumnOptions();

            var columnOptions = new ColumnOptions
            {
                Id = { ColumnName = "Id", DataType = SqlDbType.Int, AllowNull = false, DataLength = 128 },
                Level = { ColumnName = "Level", DataType = SqlDbType.NVarChar, AllowNull = false, DataLength = 128 },
                TimeStamp = { ColumnName = "TimeStamp", DataType = SqlDbType.DateTimeOffset, AllowNull = false },
                LogEvent = { ColumnName = "LogEvent", DataType = SqlDbType.NVarChar, AllowNull = true, DataLength = 4000 },
                Message = { ColumnName = "Message", DataType = SqlDbType.NVarChar, AllowNull = true, DataLength = 4000 },
                Exception = { ColumnName = "Exception", DataType = SqlDbType.NVarChar, AllowNull = true, DataLength = 4000 },
                AdditionalColumns = usernameColumnOptions.AdditionalColumns.Concat(methodNameColumnOptions.AdditionalColumns).ToList()
            };

            columnOptions.Store.Remove(StandardColumn.MessageTemplate);

            Serilog.Core.Logger seriLogConfig = new LoggerConfiguration().WriteTo
                .MSSqlServer(
                    logConfiguration.ConnectionString,
                    sinkOptions,
                    columnOptions: columnOptions)
                    .Enrich.FromLogContext()
                    .Enrich.With<CustomUsernameColumnEvent>()
                    .Enrich.With<CustomMethodNameColumnEvent>()
                    .MinimumLevel.Information()
                    .CreateLogger();

            Logger = seriLogConfig;
        }
        catch (Exception ex)
        {
            // Hata mesajını logla
            Console.WriteLine($"Error initializing MsSqlLogger: {ex.Message}");
            throw;
        }
    }
}
