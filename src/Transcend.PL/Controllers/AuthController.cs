using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.Login;
using Transcend.Common.Models.User;
using Transcend.Common.Utilities;

namespace Transcend.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    private readonly ITokenService tokenService;

    public AuthController (IAuthService authService, ITokenService tokenService)
    {
        this.authService = authService;
        this.tokenService = tokenService;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<Response>> RegisterUserAsync([FromBody]UserIM userIM)
    {
        if(await this.authService.CheckIfUserExistsAsync(userIM.Username, userIM.Email)) 
            return this.BadRequest(new Response 
            { 
                Status = "User creation failure", 
                Message = "This user already exists!" 
            });

        var response = await this.authService.CreateUserAsync(userIM);

        if(!response.Succeeded) 
            return this.BadRequest(new Response
            {
                Status = "User creation failure",
                Message = response.Errors.First().Description,
            });

        return this.Ok(
            new Response
            {
                Status = "User created successfully",
                Message = "This user has been created successfully!"
            });
    }

    [HttpPost("Login")]
    public async Task<ActionResult<LoginVM>> LoginUserAsync([FromBody]LoginIM loginIM)
    {
        if (!await this.authService.CheckIfUserExistsAsync(loginIM.Username))
            return this.NotFound(new Response
            {
                Status = "User not found",
                Message = "This user does not exist!"
            });

        if (!await this.authService.CheckIfPasswordIsCorrectAsync(loginIM.Username, loginIM.Password))
            return this.BadRequest(new Response
            { 
                Status = "Incorrect password",
                Message = "The password is incorrect!"
            });

        var token = await this.tokenService.CreateTokenForUserAsync(loginIM.Username);

        return this.Ok(
            new LoginVM 
            { 
                Status = "User logged in successfully",
                Message = "This user has been logged in successfully!",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });
    }
}
