using Microsoft.AspNetCore.Identity;
using TrustRecruit.DataAccess.DBContext;
using TrustRecruit.DataAccess.Identity;

namespace TrustRecruit.DataAccess.Repository.Interfaces;

public interface IRoleRepository
{
  public Task<IdentityResult> CreateRoleAsync(ApplicationRole role);
  public Task<IdentityResult> AddUserToRole(ApplicationUser user,ApplicationRole role);
  public Task<ApplicationRole?> GetRoleByNameAsync(string roleName);
}
