using Microsoft.EntityFrameworkCore;
using WebApiLocationSearch.Models;

namespace WebApiLocationSearch.Data
{
    public class AppDbContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<FavoriteLocation> FavoriteLocations { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>()
                .HasOne(l => l.User)
                .WithMany(u => u.Logs)
                .HasForeignKey(l => l.UserId);
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<FavoriteLocation>().HasOne(l => l.User)
                .WithMany(u => u.FavoriteLocations)
                .HasForeignKey(l => l.UserId);
        }
    }
}