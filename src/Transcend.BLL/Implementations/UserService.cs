using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public UserService(UserManager<User> userManager, TranscendDBContext dbContext, IMapper mapper)
    {
        this.userManager = userManager;
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<UserVM> GetUserByIdAsync(string id)
    {
        return this.mapper.Map<UserVM>(await userManager.Users
            .Include(usr => usr.UserDetails)
            .Where(usr => usr.Id == id)
            .FirstOrDefaultAsync());
    }

    public async Task<UserVM> UpdateUserAsync(string id, UserUM userUM)
    {
        var user = await userManager.Users
            .Include(usr => usr.UserDetails)
            .Where(usr => usr.Id == id)
            .FirstAsync();

        if(userUM.Username != null)
            user.UserName = userUM.Username;

        if (userUM.Password != null)
        {
            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);

            _ = await this.userManager.ResetPasswordAsync(user, token, userUM.Password);
        }

        if(userUM.FirstName != null)
            user.UserDetails.FirstName = userUM.FirstName;

        if(userUM.LastName != null)
            user.UserDetails.LastName = userUM.LastName;

        if(userUM.Email != null)
            user.Email = userUM.Email;

        if(userUM.PhoneNumber != null)
            user.PhoneNumber = userUM.PhoneNumber;

        if(userUM.ShippingAddress != null)
            user.UserDetails.ShippingAddress = userUM.ShippingAddress;

        await this.userManager.UpdateAsync(user);

        return this.mapper.Map<UserVM>(user);
    }

    public async Task DeleteUserAsync(string id)
    {
        var user = await userManager.Users
            .Where(usr => usr.Id == id)
            .Include(usr => usr.UserDetails)
            .FirstAsync();

        var userDetails = await dbContext.UserDetails
            .Where(ud => ud.Id == user.UserDetails.Id)
            .FirstAsync();

        await userManager.DeleteAsync(user);

        dbContext.Remove(userDetails);

        await dbContext.SaveChangesAsync();
    }
}
