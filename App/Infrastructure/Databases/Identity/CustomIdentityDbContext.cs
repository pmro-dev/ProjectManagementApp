using App.Features.Exceptions.Throw;
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
			.UsingEntity<UserRoleModel>(
				l => l
					.HasOne(ur => ur.Role)
					.WithMany(r => r.RoleUsers)
					.HasForeignKey(ur => ur.RoleId),
				r => r
					.HasOne(ur => ur.User)
					.WithMany(u => u.UserRoles)
					.HasForeignKey(ur => ur.UserId));

		modelBuilder.Entity<RoleModel>()
			.ToTable("Roles");

		modelBuilder.Entity<UserRoleModel>()
			.ToTable("UserRoles");
		#endregion


		#region User - Team

		modelBuilder.Entity<UserModel>()
			.HasMany(u => u.Teams)
			.WithMany(r => r.Members)
			.UsingEntity<UserTeamModel>(
				l => l
					.HasOne(ut => ut.Team)
					.WithMany(t => t.TeamMembers)
					.HasForeignKey(ut => ut.TeamId),
				p => p
					.HasOne(ut => ut.Member)
					.WithMany(u => u.UserTeams)
					.HasForeignKey(ut => ut.MemberId));

		modelBuilder.Entity<UserTeamModel>()
			.ToTable("MemberTeams");
		#endregion


		#region User - Project

		modelBuilder.Entity<UserModel>()
			.HasMany(u => u.Projects)
			.WithMany(r => r.Clients)
			.UsingEntity<UserProjectModel>(
				l => l
					.HasOne(up => up.Project)
					.WithMany(t => t.ProjectClients)
					.HasForeignKey(ut => ut.ProjectId),
				p => p
					.HasOne(ut => ut.Owner)
					.WithMany(u => u.ClientProjects)
					.HasForeignKey(ut => ut.OwnerId));

		modelBuilder.Entity<UserProjectModel>()
			.ToTable("ClientProjects");

		modelBuilder.Entity<UserModel>()
			.HasMany(u => u.Projects)
			.WithOne(p => p.Owner)
			.HasForeignKey(p => p.OwnerId);
		#endregion


		#region User - TodoList

		modelBuilder.Entity<UserModel>()
			.HasMany(u => u.TodoLists)
			.WithOne(t => t.Owner)
			.HasForeignKey(t => t.OwnerId);

		modelBuilder.Entity<UserModel>()
			.HasMany(u => u.TodoLists)
			.WithOne(t => t.Creator)
			.HasForeignKey(t => t.CreatorId);
		#endregion


		#region User - Budget

		modelBuilder.Entity<UserModel>()
			.HasMany(u => u.Budgets)
			.WithOne(b => b.Owner)
			.HasForeignKey(b => b.OwnerId);
		#endregion


		#region User - Billings

		modelBuilder.Entity<UserModel>()
			.HasMany(u => u.Billings)
			.WithOne(b => b.Executor)
			.HasForeignKey(b => b.ExecutorId);
		#endregion


		#region User - Incomes

		modelBuilder.Entity<UserModel>()
			.HasMany(u => u.Incomes)
			.WithOne(b => b.Executor)
			.HasForeignKey(b => b.ExecutorId);
		#endregion


		modelBuilder.Entity<UserModel>()
			.ToTable("Users")
			.HasKey(u => u.Id);

		_logger?.LogInformation(ExceptionsMessages.LogBuildingSucceed, nameof(OnModelCreating), nameof(CustomIdentityDbContext));
	}
}
