using Pharmacy.Domain;

namespace Pharmacy.Application.Common.Interfaces;

public interface ICurrentUser
{
    Guid GetUserId();
    string? GetPhoneNumber();
    UserRole GetUserRole();
}
