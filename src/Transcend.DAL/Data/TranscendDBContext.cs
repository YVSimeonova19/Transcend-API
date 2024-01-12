﻿using Microsoft.EntityFrameworkCore;
using Transcend.DAL.Models;

namespace Transcend.DAL.Data;

public class TranscendDBContext : DbContext
{
    public TranscendDBContext(DbContextOptions<TranscendDBContext> options) : base(options)
    {

    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserDetails> UserDetails { get; set; }
    public DbSet<Carrier> Carriers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Carrier>()
            .HasMany(c => c.Users)
            .WithOne()
            .HasForeignKey(u => u.CarrierId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne()
            .HasForeignKey(o => o.UserPlaceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        base.OnModelCreating(modelBuilder);
    }
}