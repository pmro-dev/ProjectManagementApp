using App.Features.Exceptions.Throw;
using App.Features.Projects.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Projects.Models;
using App.Features.Users.Common.Roles.Models;
using App.Features.Users.Common.Teams.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Databases.Identity;

/// <summary>
/// Custom Db Context for Identities.
/// </summary>
public class CustomIdentityDbContext : DbContext
{
	private readonly ILogger<CustomIdentityDbContext>? _logger;

	public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> options, ILogger<CustomIdentityDbContext> logger)
		: base(options) { _logger = logger; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		#region User - Role

		modelBuilder.Entity<UserModel>()
			.HasMany(u => u.Roles)
			.WithMany(r => r.Users)
			.UsingEntity<UserRoleModel>();

		//modelBuilder.Entity<RoleModel>()
		//	.HasMany(r => r.Users)
		//	.WithMany(u => u.Roles)
		//	.UsingEntity<UserRoleModel>();

		modelBuilder.Entity<RoleModel>()
			.ToTable("Roles");
		#endregion


		#region User - Team

		modelBuilder.Entity<UserModel>()
			.HasMany(u => u.Teams)
			.WithMany(r => r.Members)
			.UsingEntity<UserTeamModel>(
				l => l.HasOne<TeamModel>(ut => ut.Team)
				.WithMany(t => t.TeamMembers)
				.HasForeignKey(ut => ut.TeamId),
				p => p.HasOne<UserModel>(ut => ut.Member)
				.WithMany(u => u.UserTeams)
				.HasForeignKey(ut => ut.MemberId)
			);
		#endregion


		#region User - Project

		modelBuilder.Entity<UserModel>()
			.HasMany(u => u.Projects)
			.WithMany(r => r.Clients)
			.UsingEntity<UserProjectModel>(
				l => l.HasOne<ProjectModel>(up => up.Project)
				.WithMany(t => t.ProjectClients)
				.HasForeignKey(ut => ut.ProjectId),
				p => p.HasOne<UserModel>(ut => ut.Owner)
				.WithMany(u => u.UserProjects)
				.HasForeignKey(ut => ut.OwnerId)
			);

		modelBuilder.Entity<UserModel>()
			.HasMany(u => u.Projects)
			.WithOne(p => p.Owner)
			.HasForeignKey(p => p.OwnerId);
		#endregion


		#region User - TodoList

		modelBuilder.Entity<UserModel>()
			.HasMany(u => u.TodoLists)
			.WithMany(t => t.use);
		#endregion


		modelBuilder.Entity<UserModel>()
			.HasMany(user => user.UserProjects)
			.WithOne(p => p.Owner)
			.HasForeignKey(p => p.OwnerId);

		modelBuilder.Entity<UserModel>()
			.HasMany(user => user.UserBudgets)
			.WithOne(b => b.Owner)
			.HasForeignKey(b => b.OwnerId);

		modelBuilder.Entity<UserModel>()
			.ToTable("Users")
			.HasKey(u => u.Id);


		modelBuilder.Entity<UserRoleModel>()
			.HasOne(userRole => userRole.Role)
			.WithMany(r => r.RoleUsers)
			.HasForeignKey(ur => ur.RoleId);

		modelBuilder.Entity<UserRoleModel>()
			.HasOne(userRole => userRole.User)
			.WithMany(u => u.UserRoles)
			.HasForeignKey(ur => ur.UserId);

		modelBuilder.Entity<UserRoleModel>()
			.HasKey(ur => new { ur.UserId, ur.RoleId });

		modelBuilder.Entity<UserRoleModel>()
			.ToTable("UserRoles");

		_logger?.LogInformation(ExceptionsMessages.LogBuildingSucceed, nameof(OnModelCreating), nameof(CustomIdentityDbContext));
	}
}
