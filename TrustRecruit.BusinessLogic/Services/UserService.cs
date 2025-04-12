using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using TrustRecruit.BusinessLogic.Models;
using TrustRecruit.BusinessLogic.ServiceContracts;
using TrustRecruit.DataAccess.Identity;
using TrustRecruit.DataAccess.Repository.Interfaces;

namespace TrustRecruit.BusinessLogic.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;
  private readonly IJwtService _jwtService;
  private readonly IConfiguration _configuration;
  private IRefreshTokenService _refreshTokenService;

  public UserService(IUserRepository userRepository, IJwtService jwtService, IConfiguration configuration,IRefreshTokenService refreshTokenService)
  {
    _userRepository = userRepository;
    _jwtService = jwtService;
    _configuration = configuration;
    _refreshTokenService = refreshTokenService;
  }
  public async Task<UserModel?> CreateUserAndSignInAsync(UserModel user)
  {
    ApplicationUser applicationUser = new ApplicationUser
    {
      Email = user.Email,
      UserName = user.Email,
      PersonName = user.Name,
      PhoneNumber = user.Mobile
    };
    IdentityResult userCreationResult = await _userRepository.CreateUserAsync(applicationUser, user.Password);

    if (userCreationResult.Succeeded)
    {
      await _userRepository.SignInUserAsync(applicationUser);
      string jwt = _jwtService.CreateJwtToken(applicationUser);
      DateTime jwtExpiration = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));
      string refreshToken = _refreshTokenService.CreateRefreshToken();
      DateTime refreshTokenExpiration = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["RefreshToken:EXPIRATION_DAYS"]));
      applicationUser.RefreshToken = refreshToken;
      applicationUser.RefreshTokenExpiry = refreshTokenExpiration;

      IdentityResult updateUserResult = await _userRepository.UpdateUserAsync(applicationUser);
      if (updateUserResult.Succeeded)
      {
        return new UserModel()
        {
          Id = applicationUser.Id,
          Email = applicationUser.Email,
          Name = applicationUser.PersonName,
          Mobile = applicationUser.PhoneNumber,
          JwtToken = jwt,
          RefreshToken = refreshToken,
          JwtExpiry = jwtExpiration,
          RefreshTokenExpiry = refreshTokenExpiration

        };
      }

    }

    return null; // TODO throw correct exception instead of sending null;
  }

  public async Task<UserModel?> LoginUserAsync(UserModel user)
  {
    var result = await _userRepository.PasswordSingInUserAsync(user.Email, user.Password);
    if (result.Succeeded)
    {
      ApplicationUser? applicationUser = await _userRepository.FindByEmailAsync(user.Email);
      if (applicationUser == null)
      {
        return null; // TODO Throw correct exception instead of returning null
      }
      await _userRepository.SignInUserAsync(applicationUser);
      string jwt = _jwtService.CreateJwtToken(applicationUser);
      DateTime jwtExpiration = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));
      string refreshToken = _refreshTokenService.CreateRefreshToken();
      DateTime refreshTokenExpiration = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["RefreshToken:EXPIRATION_DAYS"]));
      UserModel userModel = new UserModel
      {

        Id = applicationUser.Id,
        Email = applicationUser.Email,
        Name = applicationUser.PersonName,
        Mobile = applicationUser.PhoneNumber,
        JwtToken = jwt,
        RefreshToken = refreshToken,
        JwtExpiry = jwtExpiration,
        RefreshTokenExpiry = refreshTokenExpiration
      };
      return userModel;
    }

    return null;// TODO Throw correct exception instead of returning null
  }
}
