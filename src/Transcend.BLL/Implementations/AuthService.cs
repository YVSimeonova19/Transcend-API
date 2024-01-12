using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.User;
using Transcend.DAL.Data;
using Transcend.DAL.Models;

namespace Transcend.BLL.Implementations;

internal class AuthService : IAuthService
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly TranscendDBContext dbContext;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, TranscendDBContext dbContext)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.dbContext = dbContext;
    }

    public async Task<bool> CheckIfUserExistsAsync(string username, string email)
    {
        return await this.userManager.FindByEmailAsync(email) is not null && await this.userManager.FindByNameAsync(username) is not null;
    }

    public async Task<IdentityResult> CreateUserAsync(UserIM userIM)
    {
        UserDetails userDetails = new UserDetails()
        { 
            FirstName = userIM.FirstName,
            LastName = userIM.LastName,
            ShippingAddress = userIM.ShippingAddress,
        };

        await this.dbContext.UserDetails.AddAsync(userDetails);

        await this.dbContext.SaveChangesAsync();

        var user = new User()
        {
            Email = userIM.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = userIM.Email,
            PhoneNumber = userIM.PhoneNumber,
            UserDetailsId = userDetails.Id,
        };

        return await userManager.CreateAsync(user, userIM.Password);
    }
}
