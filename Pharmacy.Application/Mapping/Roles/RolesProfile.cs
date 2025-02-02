using Pharmacy.Application.Features.Roles.DropDown;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Mapping.Roles;

public class RolesProfile : MappingProfileBase
{
  public RolesProfile()
  {
    CreateMap<ApplicationRole, RolesDropDownQueryResponse>();
  }
}
