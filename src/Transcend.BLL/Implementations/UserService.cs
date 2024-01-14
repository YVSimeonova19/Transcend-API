using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Transcend.BLL.Contracts;
using Transcend.Common.Models.User;
using Transcend.DAL.Data;
using Transcend.DAL.Models;

namespace Transcend.BLL.Implementations;

internal class UserService : IUserService
{
    private readonly UserManager<User> userManager;
    private readonly TranscendDBContext dbContext;
    private readonly IMapper mapper;

    // Add dependency injections
    public UserService(UserManager<User> userManager, TranscendDBContext dbContext, IMapper mapper)
    {
        this.userManager = userManager;
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Retrieve a user from the DB by id asyncronously
    public async Task<UserVM> GetUserByIdAsync(string id)
    {
        return this.mapper.Map<UserVM>(await userManager.Users
            .Include(usr => usr.UserDetails)
            .Where(usr => usr.Id == id)
            .FirstOrDefaultAsync());
    }

    // Update the current users information in the DB asyncronously
    public async Task<UserVM> UpdateUserAsync(string id, UserUM userUM)
    {
        // Retrieve the user data from the DB
        var user = await userManager.Users
            .Include(usr => usr.UserDetails)
            .Where(usr => usr.Id == id)
            .FirstAsync();

        //Check if the username is changed and apply the changes
        if(userUM.Username != null)
            user.UserName = userUM.Username;

        //Check if the password is changed and apply the changes
        if (userUM.Password != null)
        {
            // Encrypt the new password
            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);

            _ = await this.userManager.ResetPasswordAsync(user, token, userUM.Password);
        }

        //Check if the first name is changed and apply the changes
        if (userUM.FirstName != null)
            user.UserDetails.FirstName = userUM.FirstName;

        //Check if the last name is changed and apply the changes
        if (userUM.LastName != null)
            user.UserDetails.LastName = userUM.LastName;
        
        //Check if the email is changed and apply the changes
        if (userUM.Email != null)
            user.Email = userUM.Email;

        //Check if the phone number is changed and apply the changes
        if (userUM.PhoneNumber != null)
            user.PhoneNumber = userUM.PhoneNumber;

        //Check if the shipping address is changed and apply the changes
        if (userUM.ShippingAddress != null)
            user.UserDetails.ShippingAddress = userUM.ShippingAddress;

        await this.userManager.UpdateAsync(user);

        return this.mapper.Map<UserVM>(user);
    }

    // Delete the current user asyncronously
    public async Task DeleteUserAsync(string id)
    {
        // Retrieve the user
        var user = await userManager.Users
            .Where(usr => usr.Id == id)
            .Include(usr => usr.UserDetails)
            .FirstAsync();

        // Retrieve the userDetails
        var userDetails = await dbContext.UserDetails
            .Where(ud => ud.Id == user.UserDetails.Id)
            .FirstAsync();

        await userManager.DeleteAsync(user);

        dbContext.Remove(userDetails);

        await dbContext.SaveChangesAsync();
    }
}
