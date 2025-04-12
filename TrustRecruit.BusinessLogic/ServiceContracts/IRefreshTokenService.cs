using TrustRecruit.BusinessLogic.Models;

namespace TrustRecruit.BusinessLogic.ServiceContracts;

public interface IRefreshTokenService
{
  public string CreateRefreshToken();
}
