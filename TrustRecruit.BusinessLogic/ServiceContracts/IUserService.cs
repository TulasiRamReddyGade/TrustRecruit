using TrustRecruit.BusinessLogic.Models;

namespace TrustRecruit.BusinessLogic.ServiceContracts;

public interface IUserService
{
  public Task<UserModel?> CreateUserAndSignInAsync(UserModel user);

  public Task<UserModel?> LoginUserAsync(UserModel user);
}
