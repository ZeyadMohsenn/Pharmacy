using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.Roles.DropDown;

public class RolesDropDownQueryHandler(
    RoleManager<ApplicationRole> rolesRepo,
    IMapper mapper)
        : BaseHandler<RolesDropDownQuery, Result<List<RolesDropDownQueryResponse>>>
{
    private readonly RoleManager<ApplicationRole> _rolesRepo = rolesRepo;
    private readonly IMapper _mapper = mapper;

  public override async Task<Result<List<RolesDropDownQueryResponse>>> Handle(
        RolesDropDownQuery request,
        CancellationToken cancellationToken)
    {
        var roles = await _rolesRepo.Roles.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<RolesDropDownQueryResponse>>(roles);
        return Result<List<RolesDropDownQueryResponse>>.Success(result);
    }
}
