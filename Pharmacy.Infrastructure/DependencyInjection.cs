using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Domain.Entities.Identity;
using Pharmacy.Infrastructure.Context;
using Pharmacy.Infrastructure.RateLimiting;
namespace Pharmacy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config
    )
    {
    return services
        .AddRateLimit(config)
        .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
        .AddHttpClient()
        .AddLocalization()
        .Configure<RequestLocalizationOptions>(_ => LocalizationConfig.GetLocalizationOptions())
        .AddTransient<ICurrentUser, CurrentUser>()
        .RegisterUnitOfWork<AppDbContext>()
        .RegisterIdentity();
            //.AddServices();
    }

    public static IServiceCollection RegisterUnitOfWork<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
        services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();
        return services;
    }

    public static IServiceCollection RegisterIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentityCore<ApplicationUser>(opt =>
        {
            opt.Password.RequireDigit = true;
            opt.Password.RequiredLength = 6;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredUniqueChars = 0;
            opt.Password.RequireLowercase = false;
            opt.User.RequireUniqueEmail = false;
            opt.SignIn.RequireConfirmedEmail =
                opt.SignIn.RequireConfirmedPhoneNumber =
                opt.SignIn.RequireConfirmedAccount =
                    false;
        });
        var identityBuilder = new IdentityBuilder(
            builder.UserType,
            typeof(ApplicationRole),
            builder.Services
        );
        identityBuilder.AddEntityFrameworkStores<AppDbContext>();
        identityBuilder.AddSignInManager<SignInManager<ApplicationUser>>();
        identityBuilder.AddRoleManager<RoleManager<ApplicationRole>>();
        services.TryAddSingleton(TimeProvider.System);

        return services;
    }

    //public static IServiceCollection AddServices(this IServiceCollection services)
    //{
    //    // return services
    //    //     .AddScoped<ISmsProvider, SmsProvider>();
    //}
}

public static class Startup
{
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        return app.UseRateLimit()
            .UseRequestCulture()
            .UseStaticFiles()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseCors("CorsPolicy");
    }

    private static IApplicationBuilder UseRequestCulture(this IApplicationBuilder app)
    {
        var localizationOptions = LocalizationConfig.GetLocalizationOptions();
        app.UseRequestLocalization(localizationOptions);
        return app;
    }
}

public static class LocalizationConfig
{
    public static RequestLocalizationOptions GetLocalizationOptions()
    {
        var supportedCultures = new[] { "en", "ar" };
        return new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);
    }
}
