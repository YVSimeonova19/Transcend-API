using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transcend.DAL
{
    public class Carrier
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Id")]
        [InverseProperty("Carrier")]
        public virtual ICollection<User> Users { get; set; }
    }
}
