using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transcend.Common.Models.User;

namespace Transcend.BLL.Contracts;

public interface IUserService
{
    Task<UserVM> GetUserByIdAsync(string id);

    Task<UserVM> UpdateUserAsync(string id, UserUM userUM);

    Task DeleteUserAsync(string id);
}
