using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.AccountsManagement.Command.Create;

public class CreateAccountCommand : IRequest<Result<string>>
{
    public required string FullName { get; set; }
    public string? Email { get; set; }
    public required string UserName { get; set; }
    public  string? PhoneNumber { get; set; }
    public required Guid RoleId { get; set; }
    public required string Password { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? StartDate { get; set; } // Start date of the user to work in the system
    public DateTime? EndDate { get; set; } // End date of the user to work in the system
}
