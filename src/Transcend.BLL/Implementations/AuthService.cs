using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, TranscendDBContext dbContext, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.dbContext = dbContext;
        this.roleManager = roleManager;
    }

    public async Task<bool> CheckIfPasswordIsCorrectAsync(string username, string password)
    {
        var user = await this.userManager.FindByNameAsync(username);

        if (user is null) 
            return false;

        return await this.userManager.CheckPasswordAsync(user, password);
    }

    public async Task<bool> CheckIfUserExistsAsync(string username, string email)
    {
        return await this.userManager.FindByEmailAsync(email) is not null && await this.userManager.FindByNameAsync(username) is not null;
    }

    public async Task<bool> CheckIfUserExistsAsync(string username)
    {
        return await this.userManager.FindByNameAsync(username) is not null;
    }

    public async Task<bool> CheckIfUserExistsByIdAsync(string id)
    {
        return await this.userManager.FindByIdAsync(id) is not null;
    }

    public async Task<IdentityResult> CreateCarrierAsync(CarrierIM carrierIM)
    {
        Carrier carrier = new Carrier()
        {
            Name = carrierIM.Name
        };

        var user = new User()
        {
            UserName = carrierIM.Username,
            Carrier = carrier,
            UserDetails = null
        };

        var result = await userManager.CreateAsync(user, carrierIM.Password);

        if (!result.Succeeded)
        {
            return result;
        }

        if (!await this.roleManager.RoleExistsAsync(UserRoles.Carrier))
        {
            await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Carrier));
        }

        if (await this.roleManager.RoleExistsAsync(UserRoles.Carrier))
        {
            await this.userManager.AddToRoleAsync(user, UserRoles.Carrier);
        }

        return result;
    }

    public async Task<IdentityResult> CreateUserAsync(UserIM userIM)
    {
        UserDetails userDetails = new UserDetails()
        { 
            FirstName = userIM.FirstName,
            LastName = userIM.LastName,
            ShippingAddress = userIM.ShippingAddress,
        };

        //await this.dbContext.UserDetails.AddAsync(userDetails);

        //await this.dbContext.SaveChangesAsync();

        var user = new User()
        {
            Email = userIM.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = userIM.Username,
            PhoneNumber = userIM.PhoneNumber,
            UserDetails = userDetails,
        };

        var result =  await userManager.CreateAsync(user, userIM.Password);

        if (!result.Succeeded)
        {
            //this.dbContext.UserDetails.Remove(userDetails);
            //await this.dbContext.SaveChangesAsync();
            return result;
        }

        if (!await this.roleManager.RoleExistsAsync(UserRoles.Customer))
        {
            await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));
        }

        if (await this.roleManager.RoleExistsAsync(UserRoles.Customer))
        {
            await this.userManager.AddToRoleAsync(user, UserRoles.Customer);
        }

        return result;
    }
}
