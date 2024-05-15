using JCarrollOnlineV2.Entities;
using Microsoft.EntityFrameworkCore;

namespace jcarrollonline.react.backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Forum> Forum { get; set; }
    }
}
