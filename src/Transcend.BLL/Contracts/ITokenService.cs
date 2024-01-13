using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcend.BLL.Contracts;

public interface ITokenService
{
    Task<JwtSecurityToken> CreateTokenForUserAsync(string username);
}
