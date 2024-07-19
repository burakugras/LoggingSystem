
using Business;
using Core.DependencyResolvers;
using Core.Utilities.IoC;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.Security;
using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Core.Extensions;
using Core.CrossCutingConcerns.Exceptions.Extensions;
using WebAPI.Utilities;
using WebAPI.Filters;
using Autofac.Core;

namespace WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<ActivityLoggerService>();
        builder.Services.AddScoped<LoggingFilter>();

        // Add services to the container.
        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add<LoggingFilter>(); // Add the logging filter globally
        });

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });

        builder.Services.AddControllers();
        builder.Services.AddBusinessServices();
        builder.Services.AddDataAccessServices(builder.Configuration);
        builder.Services.AddValidationAspect();

        var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidIssuer = tokenOptions.Issuer,
                                ValidAudience = tokenOptions.Audience,
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                            };
                        });


        builder.Services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description =
                    "Please enter token"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
            { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
        new string[] { }
    }
});
        });

        builder.Services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //middleware kodu buraya eklenecek

        app.ConfigureCustomExceptionMiddleware();//s�raya dikkat et MapControllers'�n alt�nda olabilir

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
