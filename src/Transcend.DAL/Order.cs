using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transcend.DAL
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public DateTime ExpectedDeliveryDate { get; set; }

        [ForeignKey("Carrier")]
        public int CarrierId { get; set; }
        public virtual Carrier Carrier { get; set; }

        [ForeignKey("User")]
        public int UserPlaceId { get; set; }
        public virtual User User { get; set; }
    }
}
