using Microsoft.AspNetCore.Builder;
using Core.CrossCutingConcerns.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Core.CrossCutingConcerns.Logging.SeriLog;

namespace Core.CrossCutingConcerns.Exceptions.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<AuthorizationMiddleware>();
            app.UseMiddleware<ValidationMiddleware>();
            app.UseMiddleware<SeriLogMiddleware>();
        }
    }
}
