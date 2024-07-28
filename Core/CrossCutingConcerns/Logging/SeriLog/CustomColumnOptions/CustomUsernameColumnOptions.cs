using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCutingConcerns.Logging.SeriLog.CustomColumnOptions
{
    public static class CustomUsernameColumnOptions
    {
        public static ColumnOptions CreateUsernameColumnOptions()
        {
            var customColumnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn
                    {
                        ColumnName="Username",
                        DataType=SqlDbType.NVarChar,
                        DataLength=255,
                        AllowNull=true,
                    }
                }
            };
            return customColumnOptions;
        }
    }
}
