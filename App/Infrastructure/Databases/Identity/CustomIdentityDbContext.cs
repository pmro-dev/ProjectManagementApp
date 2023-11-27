using App.Features.Exceptions.Throw;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Databases.Identity;

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
			.HasMany(role => role.UserRoles)
			.WithOne(s => s.Role);

		modelBuilder.Entity<RoleModel>()
			.ToTable("Roles");


		modelBuilder.Entity<UserModel>()
			.HasMany(user => user.UserRoles)
			.WithOne(s => s.User);

		modelBuilder.Entity<UserModel>()
			.ToTable("Users");


		modelBuilder.Entity<UserRoleModel>()
			.HasOne(userRole => userRole.Role)
			.WithMany(s => s.UserRoles);

		modelBuilder.Entity<UserRoleModel>()
		.HasOne(userRole => userRole.User)
		.WithMany(userModel => userModel.UserRoles);

		modelBuilder.Entity<UserRoleModel>()
			.HasKey(ur => new { ur.UserId, ur.RoleId });

		modelBuilder.Entity<UserRoleModel>()
			.ToTable("UserRoles");


		_logger?.LogInformation(ExceptionsMessages.LogBuildingSucceed, nameof(OnModelCreating), nameof(CustomIdentityDbContext));
	}
}
