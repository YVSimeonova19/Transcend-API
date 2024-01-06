using System.Data.Entity;

namespace Transcend.DAL
{
    public class TranscendDBContext : DbContext
    {
        public TranscendDBContext() : base("TranscendDB")
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
    }
}
