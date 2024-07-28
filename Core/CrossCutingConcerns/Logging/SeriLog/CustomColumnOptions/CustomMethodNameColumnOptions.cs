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
    public static class CustomMethodNameColumnOptions
    {
        public static ColumnOptions CreateMethodNameColumnOptions()
        {
            var methodNameColumnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new SqlColumn
                    {
                        ColumnName="MethodName",
                        DataType=SqlDbType.NVarChar,
                        DataLength=4000,
                        AllowNull=true,
                    }
                }
            };
            return methodNameColumnOptions;
        }
    }
}
