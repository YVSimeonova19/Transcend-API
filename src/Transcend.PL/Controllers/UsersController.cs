using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.User;

namespace Transcend.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Customer")]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;
    private readonly IAuthService authService;
    private readonly ICurrentUser currentUser;

    // Add dependency injections
    public UsersController (IUserService userService, IAuthService authService, ICurrentUser currentUser)
    {
        this.userService = userService;
        this.authService = authService;
        this.currentUser = currentUser;
    }

    // Display the current users information asyncronously
    [HttpGet]
    public async Task<ActionResult<UserVM>> DisplayUserDataAsync()
    {
       return await userService.GetUserByIdAsync(currentUser.UserId);
    }

    // Update the current user asyncronously
    [HttpPatch]
    public async Task<ActionResult<UserVM>> EditUser([FromBody]UserUM userUM)
    {
        if (!await this.authService.CheckIfUserExistsByIdAsync(this.currentUser.UserId))
            return NotFound();

        return await userService.UpdateUserAsync(currentUser.UserId, userUM);
    }

    // Delete the current user asyncronously
    [HttpDelete]
    public async Task DeleteUser()
    {
        await this.userService.DeleteUserAsync(currentUser.UserId);
    }
}
