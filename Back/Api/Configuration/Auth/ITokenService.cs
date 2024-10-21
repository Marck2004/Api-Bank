using Cobo.Application.Dtos.Users;

namespace Cobo.Api.Configuration.Auth;
public interface ITokenService
{
    public string CreateToken(QueriesUserDto user);
}

