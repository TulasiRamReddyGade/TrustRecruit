using System.Security.Cryptography;
using TrustRecruit.BusinessLogic.ServiceContracts;

namespace TrustRecruit.BusinessLogic.Services;

public class RefreshTokenService: IRefreshTokenService
{
  public string CreateRefreshToken()
  {
    byte[] bytes = new byte[64];
    var randomNumberGenerator = RandomNumberGenerator.Create();
    return Convert.ToBase64String(bytes);
  }
}
