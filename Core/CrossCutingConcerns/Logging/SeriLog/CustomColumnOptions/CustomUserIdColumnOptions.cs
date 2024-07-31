using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace Core.CrossCutingConcerns.Logging.SeriLog.CustomColumnOptions
{
    public static class CustomUserIdColumnOptions
    {
        public static ColumnOptions CreateUserIdColumnOptions()
        {
            var customColumnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn
                    {
                        ColumnName="UserId",
                        DataType=SqlDbType.NVarChar,
                        DataLength=128,
                        AllowNull=true,
                    }
                }
            };
            return customColumnOptions;
        }
    }
}
