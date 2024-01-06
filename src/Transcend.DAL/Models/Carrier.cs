using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transcend.DAL.Models;

public class Carrier
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [ForeignKey("Id")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
