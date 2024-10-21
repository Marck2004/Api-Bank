using Cobo.Application.Dtos.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cobo.Api.Configuration.Auth;
public class TokenService : ITokenService
{
    private IConfiguration _configuration;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string CreateToken(QueriesUserDto user)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

        List<Claim> claims = new()
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.email),
            new(JwtRegisteredClaimNames.Email, user.email),
            new("userId", user.Id.ToString()),
        };

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        string jwt = tokenHandler.WriteToken(token);

        return jwt;
    }
}

