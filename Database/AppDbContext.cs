using Microsoft.EntityFrameworkCore;
using WebApiLocationSearch.Models;

namespace WebApiLocationSearch.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}