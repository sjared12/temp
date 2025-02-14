// File: Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using WeddingShare.Models;

namespace WeddingShare.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
