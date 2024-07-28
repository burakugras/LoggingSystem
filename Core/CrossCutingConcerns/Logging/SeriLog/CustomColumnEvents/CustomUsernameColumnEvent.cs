using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCutingConcerns.Logging.SeriLog.CustomColumnEvents
{
    public class CustomUsernameColumnEvent : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Properties is not null)
            {
                var (username, value) = logEvent.Properties.FirstOrDefault(x => x.Key == "Username");
                if (value is not null)
                {
                    var getValue = propertyFactory.CreateProperty(username, value);
                    logEvent.AddPropertyIfAbsent(getValue);
                }
            }
        }
    }
}
