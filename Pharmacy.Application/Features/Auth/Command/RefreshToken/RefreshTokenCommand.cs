using MediatR;
using Pharmacy.Application.Services.TokenService;
using Pharmacy.Domain.Dto;

namespace Pharmacy.Application.Features.Auth.Command.RefreshToken;

public class RefreshTokenCommand : IRequest<Result<TokenDto>>
{
  public string RefreshToken { get; set; } = null!;
}
