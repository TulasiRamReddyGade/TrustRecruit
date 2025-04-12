using Microsoft.AspNetCore.Identity;
using TrustRecruit.DataAccess.DBContext;
using TrustRecruit.DataAccess.Identity;
using TrustRecruit.DataAccess.Repository.Interfaces;

namespace TrustRecruit.DataAccess.Repository;

public class RoleRepository : IRoleRepository
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly RoleManager<ApplicationRole> _roleManager;
  private readonly ApplicationDbContext _dbContext;

  public RoleRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,RoleManager<ApplicationRole> roleManager, ApplicationDbContext dbContext)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _roleManager = roleManager;
    _dbContext = dbContext;
  }

  public async Task<IdentityResult> CreateRoleAsync(ApplicationRole role)
  {
    IdentityResult result = await _roleManager.CreateAsync(role);
    return result;
  }

  public async Task<IdentityResult> AddUserToRole(ApplicationUser user,ApplicationRole role)
  {
    IdentityResult result = await _userManager.AddToRoleAsync(user, role.Name ?? "");
    return result;
  }

  public async Task<ApplicationRole?> GetRoleByNameAsync(string roleName)
  {
    ApplicationRole? role =  await _roleManager.FindByNameAsync(roleName);
    return role;
  }
}
