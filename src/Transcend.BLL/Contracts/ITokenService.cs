using System.IdentityModel.Tokens.Jwt;

namespace Transcend.BLL.Contracts;

public interface ITokenService
{
    // Create a token for the logged user asyncronously
    Task<JwtSecurityToken> CreateTokenForUserAsync(string username);
}
