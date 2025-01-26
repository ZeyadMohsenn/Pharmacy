using Pharmacy.Application.Services.TokenService;

namespace Pharmacy.APIs.Extensions;

public static class ConfigurationExtensions
{
    public static void AddApplicationConfigurations(this WebApplicationBuilder builder)
    {
        // var fileSettings = builder.Configuration.GetSection("FileSettings").Get<FileSettings>();
        var authSettings = builder.Configuration.GetSection("AuthSettings").Get<AuthSettings>();

        // builder.Services.AddSingleton(fileSettings!);
        builder.Services.AddSingleton(authSettings!);
    }
}

