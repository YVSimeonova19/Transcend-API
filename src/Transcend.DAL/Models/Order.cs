using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Transcend.Common.Utilities;

namespace Transcend.DAL.Models;

public class Order
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public OrderStatus Status { get; set; }

    [Required]
    public DateTime ExpectedDeliveryDate { get; set; }

    [Required]
    [ForeignKey(nameof(Carrier))]
    public string CarrierId { get; set; } = string.Empty;
    public virtual User? Carrier { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public string UserPlaceId { get; set; } = String.Empty;
    public virtual User? User { get; set; }
}
