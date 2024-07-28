using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCutingConcerns.Logging
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class LoggingAttribute : Attribute
    {
        public Type LoggingType { get; set; }

        public LoggingAttribute(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new Exception("The wrong type of logger was passed");
            }
            LoggingType = loggerService;
        }
    }
}
