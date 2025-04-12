using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TrustRecruit.BusinessLogic.Models;
using TrustRecruit.BusinessLogic.ServiceContracts;
using TrustRecruit.DataAccess.Identity;
using TrustRecruit.DataAccess.Repository.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TrustRecruit.BusinessLogic.Services;

public class JwtService : IJwtService
{
  private readonly IConfiguration _configuration;
  public JwtService(IConfiguration configuration)
  {
    _configuration = configuration;
  }
  public string CreateJwtToken(ApplicationUser user)
  {
    DateTime expirationDate = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

    Claim[] claims = new Claim[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
      new Claim(ClaimTypes.NameIdentifier,user.Email),
      new Claim(ClaimTypes.Name, user.PersonName),
      new Claim(ClaimTypes.Email, user.Email)
    };


    SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
    SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    JwtSecurityToken tokenGenerator = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],claims, expires: expirationDate,signingCredentials:credentials);

    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
    string token = tokenHandler.WriteToken(tokenGenerator);

    return token;
  }
}
