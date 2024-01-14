using System.ComponentModel.DataAnnotations;

namespace Transcend.Common.Models.Login;

public class LoginIM
{
    [Required]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Яа-яa-z])([A-ZА-Яа-яa-z]{2,29})+(?<![_.])$", ErrorMessage = "Username is not valid")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
