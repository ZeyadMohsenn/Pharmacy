namespace Pharmacy.Application.Services.TokenService;

public class TokenDto
{
  public string AccessToken { get; set; } = null!;
  public string RefreshToken { get; set; } = null!;
  public DateTime Expires { get; set; }
}
