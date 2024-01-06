using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transcend.DAL.Models;

public class Order
{
    [Required]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime ExpectedDeliveryDate { get; set; }

    [ForeignKey(nameof(Carrier))]
    public int CarrierId { get; set; }
    public virtual Carrier Carrier { get; set; } = new Carrier();

    [ForeignKey(nameof(User))]
    public int UserPlaceId { get; set; }
    public virtual User User { get; set; } = new User();
}
