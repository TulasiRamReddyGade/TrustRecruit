using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrustRecruit.DataAccess.DBContext;
using TrustRecruit.DataAccess.Identity;
using TrustRecruit.DataAccess.Repository.Interfaces;

namespace TrustRecruit.DataAccess.Repository;

public class UserRepository : IUserRepository
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly RoleManager<ApplicationRole> _roleManager;
  private readonly ApplicationDbContext _dbContext;

  public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,RoleManager<ApplicationRole> roleManager, ApplicationDbContext dbContext)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _roleManager = roleManager;
    _dbContext = dbContext;
  }


  public async Task<IdentityResult> CreateUserAsync(ApplicationUser user,string password)
  {
    IdentityResult result = await _userManager.CreateAsync(user, password);
    return result;
  }

  public async Task SignInUserAsync(ApplicationUser user)
  {
    await _signInManager.SignInAsync(user, false);
  }

  public async Task<SignInResult> PasswordSingInUserAsync(string email, string password)
  {
    return await _signInManager.PasswordSignInAsync(email,password,isPersistent:false,lockoutOnFailure:false);
  }

  public async Task<ApplicationUser?> FindByEmailAsync(string email)
  {
    return await _userManager.FindByEmailAsync(email);
  }

  public async Task<IdentityResult> UpdateUserAsync(ApplicationUser applicationUser)
  {
    IdentityResult result = await _userManager.UpdateAsync(applicationUser);
    return result;
  }
}
