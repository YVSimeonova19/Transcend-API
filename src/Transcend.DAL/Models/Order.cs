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
    public int CarrierId { get; set; }
    public virtual Carrier Carrier { get; set; } = new Carrier();

    [Required]
    [ForeignKey(nameof(User))]
    public string UserPlaceId { get; set; } = String.Empty;
    public virtual User User { get; set; } = new User();
}
