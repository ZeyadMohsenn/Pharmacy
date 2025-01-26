using Pharmacy.Domain;

namespace Pharmacy.APIs.Extensions;

public static class IAuthorizationPolicyConfig
{
    public static void AddPolicy(this IServiceCollection services)
    {
        services.AddAuthorization(opt =>
        {
            opt.AddPolicy(nameof(PolicyName.Admin), policy => policy.RequireRole(nameof(UserRole.SuperAdmin) , nameof(UserRole.Admin)));
        });
    }
}

public enum PolicyName
{
    Admin,
    User
}
