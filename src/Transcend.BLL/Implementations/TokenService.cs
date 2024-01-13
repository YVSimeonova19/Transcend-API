using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Transcend.BLL.Contracts;
using Transcend.DAL.Models;

namespace Transcend.BLL.Implementations;

internal class TokenService : ITokenService
{
    private readonly UserManager<User> userManager;
    private readonly IConfiguration configuration;

    public TokenService(UserManager<User> userManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.configuration = configuration;
    }

    public async Task<JwtSecurityToken> CreateTokenForUserAsync(string username)
    {
        var user = await userManager.FindByNameAsync(username);

        var role = await userManager.GetRolesAsync(user);

        var authClaims = new List<Claim> { 
            new Claim(ClaimTypes.NameIdentifier, user.Id), 
            new Claim(ClaimTypes.Role, role.First())
        };

        return this.CreateToken(authClaims);
    }

    private JwtSecurityToken CreateToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JWT:Secret"]));
        _ = int.TryParse(this.configuration["JWT:AccessTokenValidityInMinutes"], out int tokenValidity);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(tokenValidity),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        return token;
    }
}
