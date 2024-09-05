using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCutingConcerns.Logging.DecriptionTools
{
    public class ActivityDescription
    {
        public string ActivityType { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public List<Parameter> Parameters { get; set; }
        public string UserId { get; set; }
    }
}
