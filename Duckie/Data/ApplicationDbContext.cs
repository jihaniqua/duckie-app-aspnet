using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Duckie.Models;

namespace Duckie.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Duckie.Models.ChildProfile> ChildProfile { get; set; } = default!;
        public DbSet<Duckie.Models.Milestone> Milestone { get; set; } = default!;
    }
}