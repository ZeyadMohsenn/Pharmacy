using Microsoft.AspNetCore.Identity;

namespace Pharmacy.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string Full_Name { get; set; } = null!;
    public bool Is_Active { get; set; } = true;
    public bool Is_Deleted { get; set; } = false;
    public DateTime? Date_Of_Birth { get; set; }
    public Guid Created_By { get; set; }
    public DateTime Created_At { get; set; }
    public Guid? Modified_By { get; set; }
    public DateTime? Modified_At { get; set; }
    public DateTime? Start_Date { get; set; } // Start date of the user to work in the system
    public DateTime? End_Date { get; set; } // End date of the user to work in the system
    public DateTime? Last_Login { get; set; }
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
}
