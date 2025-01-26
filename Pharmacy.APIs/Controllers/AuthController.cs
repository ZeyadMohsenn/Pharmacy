using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Features.Auth.Command.Login;
using Pharmacy.Application.Features.Auth.Command.RefreshToken;
using Pharmacy.Application.Services.TokenService;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs.Controllers;

[AllowAnonymous]
public class AuthController(ISender sender) : BaseApiController
{
    private readonly ISender _mediator = sender;

    [HttpPost("login")]
    public async Task<ActionResult<Result<TokenDto>>> Login(LoginCommand command)
    {
        return BaseResponseHandler(await _mediator.Send(command));
    }

    [HttpPost("refreshToken")]
    public async Task<ActionResult<Result<TokenDto>>> RefreshToken(RefreshTokenCommand command)
    {
        var response = await _mediator.Send(command);
        if (response.Succeeded == false)
            return Unauthorized();
        return BaseResponseHandler(response);
    }


}
