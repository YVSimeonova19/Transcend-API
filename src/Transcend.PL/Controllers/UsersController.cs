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

    public UsersController (IUserService userService, IAuthService authService, ICurrentUser currentUser)
    {
        this.userService = userService;
        this.authService = authService;
        this.currentUser = currentUser;
    }

    [HttpGet]
    public async Task<ActionResult<UserVM>> DisplayUserDataAsync()
    {
       return await userService.GetUserByIdAsync(currentUser.UserId);
    }

    [HttpPatch]
    public async Task<ActionResult<UserVM>> EditUser([FromBody]UserUM userUM)
    {
        if (!await this.authService.CheckIfUserExistsByIdAsync(this.currentUser.UserId))
            return NotFound();

        return await userService.UpdateUserAsync(currentUser.UserId, userUM);
    }

    [HttpDelete]
    public async Task DeleteUser()
    {
        await this.userService.DeleteUserAsync(currentUser.UserId);
    }
}
