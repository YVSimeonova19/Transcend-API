using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transcend.DAL
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        [ForeignKey("Carrier")]
        [Column("CarrierId")]
        public int CarrierId { get; set; }
        public virtual Carrier Carrier { get; set; }

        [ForeignKey("UserDetails")]
        public int UserDetailsId { get; set; }
        public virtual UserDetails UserDetails { get; set; }
    }
}
