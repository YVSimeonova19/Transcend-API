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
    Task<bool> CheckIfUserExistsAsync(string username, string email);

    Task<bool> CheckIfUserExistsAsync(string username);

    Task<bool> CheckIfUserExistsByIdAsync(string id);

    Task<IdentityResult> CreateUserAsync(UserIM userIM);

    Task<bool> CheckIfPasswordIsCorrectAsync(string username, string password);

    Task<IdentityResult> CreateCarrierAsync(CarrierIM carrierIM);
}
