using MediatR;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Roles.DropDown;

public class RolesDropDownQuery : IRequest<Result<List<RolesDropDownQueryResponse>>>
{

}
