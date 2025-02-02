using Pharmacy.Application.Features.AccountsManagement.Command.Create;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Mapping.AccountsManagement;

public class AccountsManagementProfile : MappingProfileBase
{
  public AccountsManagementProfile()
  {
    CreateMap<CreateAccountCommand, ApplicationUser>()
      .ForMember(dest => dest.UserRoles, opt => opt.Ignore());
  }
}
