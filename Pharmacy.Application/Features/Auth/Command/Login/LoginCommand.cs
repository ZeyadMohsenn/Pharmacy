using MediatR;
using Pharmacy.Application.Services.TokenService;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Auth.Command.Login;

public class LoginCommand : IRequest<Result<TokenDto>>
{
  public required string UserName { get; set; }
  public required string Password { get; set; }
}
