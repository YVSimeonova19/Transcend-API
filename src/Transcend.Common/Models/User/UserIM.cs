using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcend.Common.Models.User;

public class UserIM
{
    [Required]
    public string Username { get; set; } = String.Empty;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = String.Empty;

    [Required(ErrorMessage = "First name is required")]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Я])([a-zа-я]{2,29})+(?<![_.])$", ErrorMessage = "First name is not valid")]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = String.Empty;


    [Required(ErrorMessage = "Last name is required")]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Я])([a-zа-я]{2,29})+(?<![_.])$", ErrorMessage = "Last name is not valid")]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = String.Empty;

    [Required(ErrorMessage = "Email name is required")]
    [EmailAddress(ErrorMessage = "Email name is not in the correct format")]
    public string Email { get; set; } = String.Empty;

    [Required]
    [Phone]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; } = String.Empty;

    [Required(ErrorMessage = "Shipping address is required")]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Я])([a-zа-я]{2,79})+(?<![_.])$", ErrorMessage = "Shipping address is not valid")]
    [Display(Name = "Shipping address")]
    public string ShippingAddress { get; set; } = String.Empty;
}
