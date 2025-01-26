using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Services.TokenService;
using Pharmacy.Domain.Entities.Auth;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Services.Implementation;

public class TokenService(AuthSettings authSettings, IUnitOfWork unitOfWork) : ITokenService
{
  private readonly AuthSettings _authSettings = authSettings;
  private readonly IGenericRepository<RefreshToken> _refreshTokenRepository =
    unitOfWork.GetRepository<RefreshToken>();

  public async Task<TokenDto> GenerateToken(ApplicationUser user)
  {
    var accessToken = GenerateAccessToken(user);
    var refreshToken = GenerateRefreshToken(user);

    // Save the refresh token to the database
    await _refreshTokenRepository.AddAsync(refreshToken);
    await unitOfWork.SaveChangesAsync();

    return new TokenDto
    {
      AccessToken = accessToken,
      RefreshToken = refreshToken.Token,
      Expires = DateTime.UtcNow.AddMinutes(_authSettings.AccessTokenExpiryInMinutes),
    };
  }

  private string GenerateAccessToken(ApplicationUser user)
  {
    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authSettings.TokenSecretKey));
    var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

    List<Claim> claims =
    [
      new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
      new Claim(ClaimTypes.Role, user.Role.ToString()),
    ];

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = new JwtSecurityToken(
      claims: claims,
      expires: DateTime.UtcNow.AddMinutes(_authSettings.AccessTokenExpiryInMinutes),
      signingCredentials: signingCredentials
    );

    var accessToken = tokenHandler.WriteToken(token);
    return accessToken;
  }

  private RefreshToken GenerateRefreshToken(ApplicationUser user)
  {
    var randomNumber = new byte[32];
    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(randomNumber);

    // add salt to the token to make it more secure and unique
    var saltedToken = $"{Convert.ToBase64String(randomNumber)}-{DateTime.UtcNow.Ticks}";
    var finalToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(saltedToken));

    return new RefreshToken
    {
      Token = finalToken,
      Expires_On = DateTime.UtcNow.AddMinutes(_authSettings.RefreshTokenExpiryInMinutes),
      Created_At = DateTime.UtcNow,
      User_Id = user.Id,
    };
  }
}
