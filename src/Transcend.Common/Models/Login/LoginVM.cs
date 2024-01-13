using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcend.Common.Models.Login;

public class LoginVM
{
    public string Status { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string? AccessToken { get; set; }

    public DateTime? Expiration { get; set; }
}
