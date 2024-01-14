using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Transcend.BLL.Contracts;
using Transcend.DAL.Models;

namespace Transcend.BLL.Implementations;

internal class TokenService : ITokenService
{
    private readonly UserManager<User> userManager;
    private readonly IConfiguration configuration;

    // Add dependency injections
    public TokenService(UserManager<User> userManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.configuration = configuration;
    }

    // Create a token for the logged user asyncronously
    public async Task<JwtSecurityToken> CreateTokenForUserAsync(string username)
    {
        // Get the user from the DB
        var user = await userManager.FindByNameAsync(username);

        // Get the users role
        var role = await userManager.GetRolesAsync(user);

        // Create authorization claims
        var authClaims = new List<Claim> { 
            new Claim(ClaimTypes.NameIdentifier, user.Id), 
            new Claim(ClaimTypes.Role, role.First())
        };

        // Generate token
        return this.CreateToken(authClaims);
    }

    // Generate a security token
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
