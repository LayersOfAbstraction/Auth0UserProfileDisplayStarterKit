using Microsoft.EntityFrameworkCore;
using Auth0UserProfileDisplayStarterKit.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Auth0UserProfileDisplayStarterKit.Data
{
    public class TeamContext : DbContext
    {
        public TeamContext(DbContextOptions<TeamContext> options) : base(options)
        {
        }

        //Remember setting up the table to save the user to is optional.
        public DbSet<ViewModels.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //It is optional to create the user table.
            modelBuilder.Entity<ViewModels.User>().ToTable("tblUser");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccessTokenCacheConfiguration).Assembly);
        }
    }
}
