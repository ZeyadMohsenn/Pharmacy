using System.Drawing.Drawing2D;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Domain;
using Pharmacy.Domain.Entities.Identity;
using Pharmacy.Infrastructure.Context;
namespace Pharmacy.Infrastructure.Seed;

public class SeedDatabase
{
    public static async Task Seed(IUnitOfWork<AppDbContext> unitOfWork, AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        await SeedRoleData(roleManager);
        await SeedUserData(context, userManager);
        await unitOfWork.SaveChangesAsync();
    }

    private static async Task SeedUserData(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        Guid.TryParse(ApplicationConstants.Programmer, out var programmer);
        Guid.TryParse(ApplicationConstants.AdminId, out var admin);

        var user = new ApplicationUser
        {
            Id = programmer,
            Full_Name = "Super Admin",
            Email = "Programmer@programmer.com",
            NormalizedEmail = "PROGRAMMER@PROGRAMMER.COM",
            UserName = "Programmer",
            NormalizedUserName = "PROGRAMMER",
            Is_Active = true,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D"),
        };

        var Admin = new ApplicationUser
        {
            Id = admin,
            Full_Name = "Admin",
            Email = "Admin@Admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
            Is_Active = true,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D"),
        };

        var set = context.Set<ApplicationUser>();
        if (!await set.AnyAsync(a => a.Id == programmer || a.NormalizedUserName == user.NormalizedUserName))
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "P00000");
            await set.AddAsync(user);

            await context.SaveChangesAsync();
            await userManager.AddToRoleAsync(user, nameof(UserRole.SuperAdmin));
        }
        if (!await set.AnyAsync(a => a.Id == admin || a.NormalizedUserName == Admin.NormalizedUserName))
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            Admin.PasswordHash = passwordHasher.HashPassword(Admin, "P00000");
            await set.AddAsync(Admin);

            await context.SaveChangesAsync();
            await userManager.AddToRoleAsync(Admin, nameof(UserRole.SuperAdmin));
        }
    }

    private static async Task SeedRoleData(RoleManager<ApplicationRole> roleManager)
    {
        var roles = new List<ApplicationRole>();

        foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            roles.Add(new ApplicationRole{ Name = role.ToString() });

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role.Name!))
                await roleManager.CreateAsync(role);
        }
    }
}

