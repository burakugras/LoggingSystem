using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCutingConcerns.Logging
{
    public class LogDetailWithException : LogDetail
    {
        public string ExceptionMessage { get; set; }

        public LogDetailWithException()
        {
            ExceptionMessage = string.Empty;
        }

        public LogDetailWithException(string activityType, string username, string fullName, List<LogParameter> parameters, string exceptionMessage, string userId)
            : base(activityType, username, fullName, parameters, userId)
        {
            ExceptionMessage = exceptionMessage;
        }
    }
}
