using Microsoft.AspNetCore.Identity;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.Carrier;
using Transcend.Common.Models.User;
using Transcend.DAL.Data;
using Transcend.DAL.Models;

namespace Transcend.BLL.Implementations;

internal class AuthService : IAuthService
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly TranscendDBContext dbContext;
    private readonly RoleManager<IdentityRole> roleManager;

    // Add dependency injections
    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, TranscendDBContext dbContext, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.dbContext = dbContext;
        this.roleManager = roleManager;
    }

    // Check if the password a user has entered to log in is correct asyncronously
    public async Task<bool> CheckIfPasswordIsCorrectAsync(string username, string password)
    {
        // Find the user in the DB
        var user = await this.userManager.FindByNameAsync(username);

        if (user is null) 
            return false;

        // Check if password is correct
        return await this.userManager.CheckPasswordAsync(user, password);
    }

    // Check if a user exists in the DB using username and email asyncronously
    public async Task<bool> CheckIfUserExistsAsync(string username, string email)
    {
        return await this.userManager.FindByEmailAsync(email) is not null && await this.userManager.FindByNameAsync(username) is not null;
    }

    // Check if a user exists in the DB using username asyncronously
    public async Task<bool> CheckIfUserExistsAsync(string username)
    {
        return await this.userManager.FindByNameAsync(username) is not null;
    }

    // Check if a user exists in the DB using id asyncronously
    public async Task<bool> CheckIfUserExistsByIdAsync(string id)
    {
        return await this.userManager.FindByIdAsync(id) is not null;
    }

    // Create a carrier user asyncronously
    public async Task<IdentityResult> CreateCarrierAsync(CarrierIM carrierIM)
    {
        // Create a new carrier object
        Carrier carrier = new Carrier()
        {
            Name = carrierIM.Name
        };

        // Create a new user object
        var user = new User()
        {
            UserName = carrierIM.Username,
            Carrier = carrier,
            UserDetails = null
        };

        // Attempt to create user
        var result = await userManager.CreateAsync(user, carrierIM.Password);

        // If user is not created successfully
        if (!result.Succeeded)
        {
            return result;
        }

        // Create role Carrier
        if (!await this.roleManager.RoleExistsAsync(UserRoles.Carrier))
        {
            await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Carrier));
        }

        // Add role Carrier to the user
        if (await this.roleManager.RoleExistsAsync(UserRoles.Carrier))
        {
            await this.userManager.AddToRoleAsync(user, UserRoles.Carrier);
        }

        return result;
    }

    // Add a customer user to the DB asyncronously
    public async Task<IdentityResult> CreateUserAsync(UserIM userIM)
    {
        // Create a new userDetails object
        UserDetails userDetails = new UserDetails()
        { 
            FirstName = userIM.FirstName,
            LastName = userIM.LastName,
            ShippingAddress = userIM.ShippingAddress,
        };

        // Create a new user object
        var user = new User()
        {
            Email = userIM.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = userIM.Username,
            PhoneNumber = userIM.PhoneNumber,
            UserDetails = userDetails,
        };

        // Attempt to create user
        var result =  await userManager.CreateAsync(user, userIM.Password);

        // If user is not created successfully
        if (!result.Succeeded)
            return result;

        // Create role Customer
        if (!await this.roleManager.RoleExistsAsync(UserRoles.Customer))
        {
            await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));
        }

        // Add role Customer to the user
        if (await this.roleManager.RoleExistsAsync(UserRoles.Customer))
        {
            await this.userManager.AddToRoleAsync(user, UserRoles.Customer);
        }

        return result;
    }
}
