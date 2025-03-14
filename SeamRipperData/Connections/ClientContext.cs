using Microsoft.EntityFrameworkCore;
using SeamRipperData.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SeamRipperData.Connections
{
    public class ClientContext : DbContext
    {
        public DbSet<ClientInfo> Clients { get; set; }
        public DbSet<ClientMeasurements> Measurements { get; set; }

        public ClientContext(DbContextOptions<ClientContext> options) : base(options) { }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientMeasurements>()
                .HasOne(m => m.Client)
                .WithMany(c => c.Measurements)
                .HasForeignKey(m => m.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

