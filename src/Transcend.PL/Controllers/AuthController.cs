using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.User;
using Transcend.Common.Utilities;

namespace Transcend.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController (IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost]
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
}
