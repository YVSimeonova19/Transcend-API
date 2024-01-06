using System.ComponentModel.DataAnnotations;

namespace Transcend.DAL
{
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string ShippingAddress { get; set; }
    }
}
