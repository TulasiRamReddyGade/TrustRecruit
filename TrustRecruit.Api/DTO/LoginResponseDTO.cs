namespace TrustRecruit.Api.DTO;

public class LoginResponseDTO
{
  public string Email { get; set; }
  public string Name { get; set; }
  public string JwtToken { get; set; }
  public string RefreshToken { get; set; }
  public DateTime JwtExpiry { get; set; }
  public DateTime RefreshTokenExpiry { get; set; }
}
