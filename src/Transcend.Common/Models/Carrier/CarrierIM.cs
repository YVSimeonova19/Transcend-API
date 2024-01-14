using System.ComponentModel.DataAnnotations;

namespace Transcend.Common.Models.Carrier;

public class CarrierIM
{
    [Required]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Яа-яa-z])([A-ZА-Яа-яa-z]{2,29})+(?<![_.])$", ErrorMessage = "Username is not valid")]
    public string Username { get; set; } = String.Empty;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = String.Empty;

    [Required(ErrorMessage = "Name is required")]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Я])([a-zа-я]{2,29})+(?<![_.])$", ErrorMessage = "Name is not valid")]
    [Display(Name = "Name")]
    public string Name { get; set; } = String.Empty;
}
