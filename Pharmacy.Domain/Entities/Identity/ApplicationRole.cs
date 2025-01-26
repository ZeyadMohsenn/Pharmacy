using Microsoft.AspNetCore.Identity;

namespace Pharmacy.Domain.Entities.Identity;

public class ApplicationRole : IdentityRole<Guid>
{
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
}

