using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCutingConcerns.Logging
{
    public class LogDetail
    {
        public string ActivityType { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public List<LogParameter> Parameters { get; set; }
        public string UserId { get; set; } // UserId özelliği ekleniyor

        public LogDetail()
        {
            FullName = string.Empty;
            ActivityType = string.Empty;
            Username = string.Empty;
            UserId = string.Empty;
            Parameters = new List<LogParameter>();
        }

        public LogDetail(string activityType, string username, string fullName, List<LogParameter> parameters, string userId)
        {
            ActivityType = activityType;
            Username = username;
            FullName = fullName;
            Parameters = parameters;
            UserId = userId;
        }
    }
}
