using Serilog.Core;
using Serilog.Events;
using System.Linq;

namespace Core.CrossCutingConcerns.Logging.SeriLog.CustomColumnEvents
{
    public class CustomUserIdColumnEvent : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Properties is not null)
            {
                var (userId, value) = logEvent.Properties.FirstOrDefault(x => x.Key == "UserId");
                if (value is not null)
                {
                    var getValue = propertyFactory.CreateProperty(userId, value);
                    logEvent.AddPropertyIfAbsent(getValue);
                }
            }
        }
    }
}
