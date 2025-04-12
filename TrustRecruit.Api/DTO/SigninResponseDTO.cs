namespace TrustRecruit.Api.DTO;

public class SigninResponseDTO
{
  public string JwtToken { get; set; }
  public string RefreshToken { get; set; }
  public DateTime RefreshTokenExpiry { get; set; }
  public DateTime JwtExpiry { get; set; }
}
