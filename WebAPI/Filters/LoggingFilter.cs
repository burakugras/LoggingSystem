using DataAccess.Contexts;
using Entities.Concretes;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace WebAPI.Filters
{
    public class LoggingFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Execute the action
            var resultContext = await next();

            // Log the action execution details
            var httpContext = context.HttpContext;
            var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId != null && (context.Controller.GetType().Name == "AuthController" || context.Controller.GetType().Name == "UserController"))
            {
                // Resolve LogDBContext from the service provider
                var logDbContext = httpContext.RequestServices.GetService<LogDBContext>();

                if (logDbContext == null)
                {
                    throw new Exception("LogDBContext could not be resolved.");
                }

                try
                {
                    var activityLog = new Activity
                    {
                        UserId = int.Parse(userId),
                        ActivityType = context.ActionDescriptor.DisplayName,
                        Date = DateTime.UtcNow,
                        Description = $"Action {context.ActionDescriptor.DisplayName} executed."
                    };

                    logDbContext.Activities.Add(activityLog);
                    await logDbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    throw new Exception("An error occurred while saving the activity log.", ex);
                }
            }
        }
    }
}