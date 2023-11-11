using Microsoft.EntityFrameworkCore;
using Web.Accounts.Users;
using Web.Common.Helpers;

namespace Web.Databases.Identity
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
				.HasMany(role => (ICollection<UserRoleModel>)role.UserRoles)
				.WithOne(s => (RoleModel)s.Role);

			modelBuilder.Entity<RoleModel>()
				.ToTable("Roles");


			modelBuilder.Entity<UserModel>()
				.HasMany(user => (ICollection<UserRoleModel>)user.UserRoles)
				.WithOne(s => (UserModel)s.User);

			modelBuilder.Entity<UserModel>()
				.ToTable("Users");


			modelBuilder.Entity<UserRoleModel>()
				.HasOne(userRole => (RoleModel)userRole.Role)
				.WithMany(s => (ICollection<UserRoleModel>)s.UserRoles);

			modelBuilder.Entity<UserRoleModel>()
			.HasOne(userRole => (UserModel)userRole.User)
			.WithMany(userModel => (ICollection<UserRoleModel>)userModel.UserRoles);

			modelBuilder.Entity<UserRoleModel>()
				.HasKey(ur => new { ur.UserId, ur.RoleId });

			modelBuilder.Entity<UserRoleModel>()
				.ToTable("UserRoles");


			_logger?.LogInformation(MessagesPacket.LogBuildingSucceed, nameof(OnModelCreating), nameof(CustomIdentityDbContext));
		}
	}
}
