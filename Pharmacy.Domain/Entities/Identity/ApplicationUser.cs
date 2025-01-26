using Microsoft.AspNetCore.Identity;

namespace Pharmacy.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    // main properties
    public string Full_Name { get; set; } = null!;
    public UserRole Role { get; set; }
    public bool Is_Active { get; set; } = true;
    public bool Is_Deleted { get; set; } = false;
    public Guid Created_By { get; set; }
    public DateTime Created_At { get; set; }
    public Guid? Modified_By { get; set; }
    public DateTime? Modified_At { get; set; }

    // navigation properties
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
}
