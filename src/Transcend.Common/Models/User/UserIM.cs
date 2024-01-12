using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcend.Common.Models.User;

public class UserIM
{
    public string Username { get; set; } = String.Empty;

    public string Password { get; set; } = String.Empty;

    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;

    public string Email { get; set; } = String.Empty;

    public string PhoneNumber { get; set; } = String.Empty;

    public string ShippingAddress { get; set; } = String.Empty;
}
