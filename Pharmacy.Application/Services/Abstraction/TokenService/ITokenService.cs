using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Services.TokenService;

public interface ITokenService
{
  Task<TokenDto> GenerateToken(ApplicationUser user);

}
