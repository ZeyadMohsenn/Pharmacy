using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Features.AccountsManagement.Command.Create;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[AllowAnonymous]

public class AccountsController(ISender sender) : BaseApiController
{
  private readonly ISender _mediator = sender;
  [HttpPost("Create")]
  public async Task<ActionResult<Result<string>>> CreateAccount([FromBody] CreateAccountCommand command)
  {
    return BaseResponseHandler(await _mediator.Send(command));
  }

}
