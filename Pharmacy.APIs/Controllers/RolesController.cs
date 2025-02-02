using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Features.Roles.DropDown;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[AllowAnonymous]
public class RolesController(ISender sender) : BaseApiController
{
  private readonly ISender _mediator = sender;
  [HttpGet("RolesDropDown")]
  public async Task<ActionResult<Result<List<RolesDropDownQueryResponse>>>> RolesDropDown()
  {
    return BaseResponseHandler(await _mediator.Send(new RolesDropDownQuery()));
  }

}
