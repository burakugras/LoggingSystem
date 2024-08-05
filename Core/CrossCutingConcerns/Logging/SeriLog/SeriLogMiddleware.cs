using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Serilog.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace Core.CrossCutingConcerns.Logging.SeriLog
{
    public class SeriLogMiddleware
    {
        readonly RequestDelegate _next;
        public SeriLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var loggingAttributes = endpoint?.Metadata.ToList().Where(x => x.ToString().Contains(nameof(LoggingAttribute))).ToList();

            if (loggingAttributes != null)
            {
                foreach (var loggingAttribute in loggingAttributes)
                {
                    var loggingAttributeType = (LoggingAttribute)loggingAttribute;
                    var loggerType = loggingAttributeType.LoggingType;
                    var loggerService = (LoggerServiceBase)context.RequestServices.GetService(loggerType);

                    if (loggerService != null)
                    {
                        var logDetail = GetLogDetail(context);
                        loggerService.Info(logDetail);
                    }
                }
            }

            await _next(context);
        }

        private string GetLogDetail(HttpContext context)
        {
            var activityType = context.Request.Path;
            var user = context.User;
            var username = "Unauthorized";
            var userId = "Unknown";

            if (user.Identity.IsAuthenticated)
            {
                username = user.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Name)?.Value ??
                           user.Claims.FirstOrDefault(i => i.Type == JwtRegisteredClaimNames.Sub)?.Value ??
                           "Unknown";

                userId = user.Claims.FirstOrDefault(i => i.Type == "UserId")?.Value ?? "Unknown";
            }

            var logParameters = new List<LogParameter>();

            var queryStringParams = context.Request.Query;
            foreach (var param in queryStringParams)
            {
                logParameters.Add(new LogParameter()
                {
                    Name = param.Key,
                    Value = param.Value,
                    Type = "QueryString"
                });
            }

            var logDetail = new LogDetail()
            {
                ActivityType = activityType,
                Parameters = logParameters,
                Username = username,
                UserId = userId
            };

            LogContext.PushProperty("Username", username);
            LogContext.PushProperty("UserId", userId);
            LogContext.PushProperty("ActivityType", activityType);

            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(logDetail, options);
        }
    }
}
