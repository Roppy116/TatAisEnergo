using Microsoft.EntityFrameworkCore;
using TatAisEnergo.Core.Entities;

namespace TatAisEnergo.DataAccess
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<EventType> EventTypes { get; set; }

        public DbSet<History> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<History>()
                .HasOne(h => h.User)
                .WithMany()
                .HasForeignKey(h => h.UserId);

            modelBuilder.Entity<History>()
                .HasOne(h => h.EventType)
                .WithMany()
                .HasForeignKey(h => h.EventTypeId);
        }
    }
}