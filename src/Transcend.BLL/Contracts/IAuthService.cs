using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transcend.Common.Models.Carrier;
using Transcend.Common.Models.User;

namespace Transcend.BLL.Contracts;

public interface IAuthService
{
    // Check if a user exists in the DB using username and email asyncronously
    Task<bool> CheckIfUserExistsAsync(string username, string email);

    // Check if a user exists in the DB using username asyncronously
    Task<bool> CheckIfUserExistsAsync(string username);

    // Check if a user exists in the DB using id asyncronously
    Task<bool> CheckIfUserExistsByIdAsync(string id);

    // Add a customer user to the DB asyncronously
    Task<IdentityResult> CreateUserAsync(UserIM userIM);

    // Check if the password a user has entered to log in is correct asyncronously
    Task<bool> CheckIfPasswordIsCorrectAsync(string username, string password);

    // Create a carrier user asyncronously
    Task<IdentityResult> CreateCarrierAsync(CarrierIM carrierIM);
}
