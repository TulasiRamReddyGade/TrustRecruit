namespace TrustRecruit.BusinessLogic.Models;

public class UserModel
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }
  public string Mobile { get; set; }
  public string JwtToken { get; set; }
  public string RefreshToken { get; set; }
  public DateTime RefreshTokenExpiry { get; set; }
  public DateTime JwtExpiry { get; set; }
}
