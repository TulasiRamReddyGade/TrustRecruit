using TrustRecruit.BusinessLogic.Models;
using TrustRecruit.DataAccess.Identity;

namespace TrustRecruit.BusinessLogic.ServiceContracts;

public interface IJwtService
{
  public string CreateJwtToken(ApplicationUser user);
}
