using System.ComponentModel.DataAnnotations;

namespace Transcend.DAL.Models
{
    public class UserDetails
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string PhoneNumber { get; set; } = String.Empty;

        public string ShippingAddress { get; set; } = String.Empty;
    }
}
