using System.ComponentModel.DataAnnotations;

namespace Transcend.Common.Models.Order;

public class OrderIM
{
    [Required(ErrorMessage = "A name is required")]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z0-9 ])([A-ZА-Яа-яa-z])([A-ZА-Яа-яa-z0-9 ]{2,49})+(?<![_.])$", ErrorMessage = "Name is not valid")]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string CarrierId { get; set; } = string.Empty;
}
