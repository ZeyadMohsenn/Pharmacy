using System.Reflection;
using Microsoft.OpenApi.Models;
using Pharmacy.APIs.Middleware;

namespace Pharmacy.APIs.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddAPIExtensions(this IServiceCollection services)
    {
        services.AddSwagger();
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc(
                "v1",
                new OpenApiInfo { Title = "Pharmacy API", Version = "v1" }
            );
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.ResolveConflictingActions(x => x.First());
            opt.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                }
            );
            opt.AddSecurityRequirement(
                new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        Array.Empty<string>()
                    },
                }
            );
            opt.OperationFilter<AddRequiredHeaderParameter>();
            // opt.OperationFilter<FromQueryModelFilter>();
        });
    }
}
