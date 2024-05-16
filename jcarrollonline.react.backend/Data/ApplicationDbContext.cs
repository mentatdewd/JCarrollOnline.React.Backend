using jcarrollonline.react.backend.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolApp.API.Data.Models;

namespace jcarrollonline.react.backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Forum> Forum { get; set; }
        public DbSet<ForumThread> ForumThread { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
