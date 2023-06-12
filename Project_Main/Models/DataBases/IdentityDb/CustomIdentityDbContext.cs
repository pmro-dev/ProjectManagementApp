using Microsoft.EntityFrameworkCore;
using Project_IdentityDomainEntities;
using Project_Main.Infrastructure.Helpers;

namespace Project_Main.Models.DataBases.Identity
{
    /// <summary>
    /// Custom Db Context for Identities.
    /// </summary>
    public class CustomIdentityDbContext : DbContext
    {
        private readonly ILogger<CustomIdentityDbContext>? _logger;

        public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> options, ILogger<CustomIdentityDbContext> logger) : base(options)
        {
            _logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoleModel>()
                .ToTable("Roles");
            modelBuilder.Entity<UserModel>()
                .ToTable("Users");
            modelBuilder.Entity<UserRoleModel>()
                .ToTable("UserRoles");

            modelBuilder.Entity<UserRoleModel>().HasKey(ur => new { ur.UserId, ur.RoleId });

            _logger?.LogInformation(Messages.LogBuildingSucceed, nameof(OnModelCreating), nameof(CustomIdentityDbContext));
        }
    }
}
