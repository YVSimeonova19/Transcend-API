using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Transcend.DAL.Models;

namespace Transcend.DAL.Data;

public class TranscendDBContext : IdentityDbContext<User>
{
    public TranscendDBContext(DbContextOptions<TranscendDBContext> options) : base(options)
    {

    }

    // Create tables DbSets
    public DbSet<Order> Orders { get; set; }
    public DbSet<UserDetails> UserDetails { get; set; }
    public DbSet<Carrier> Carriers { get; set; }

    // Change table relationships before being created
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Create relationship between the Users and Carriers tables
        modelBuilder
            .Entity<User>()
            .Property(u => u.CarrierId)
            .IsRequired(false);

        // Create relationship between the Users and UserDetails tables
        modelBuilder
            .Entity<User>()
            .Property(u => u.UserDetailsId)
            .IsRequired(false);

        // Create relationship between the Users and Orders tables
        modelBuilder
            .Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne()
            .HasForeignKey(o => o.UserPlaceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        base.OnModelCreating(modelBuilder);
    }
}
