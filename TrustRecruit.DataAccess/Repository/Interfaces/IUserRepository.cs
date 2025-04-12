using Microsoft.AspNetCore.Identity;
using TrustRecruit.DataAccess.Identity;

namespace TrustRecruit.DataAccess.Repository.Interfaces;

public interface IUserRepository
{
  public Task<IdentityResult> CreateUserAsync(ApplicationUser user,string password);
  public Task SignInUserAsync(ApplicationUser user);
  public Task<IdentityResult> UpdateUserAsync(ApplicationUser applicationUser);
  public Task<SignInResult> PasswordSingInUserAsync(string email, string password);
  public Task<ApplicationUser?> FindByEmailAsync(string email);
}
