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

	public CustomIdentityDbContext() { }

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
					.WithMany(r => r.UserRoles)
					.HasForeignKey(ur => ur.RoleId),
				r => r
					.HasOne(ur => ur.User)
					.WithMany(u => u.UserRoles)
					.HasForeignKey(ur => ur.UserId));

		modelBuilder.Entity<UserRoleModel>()
			.ToTable("UserRoles")
			.HasKey(ur => new { ur.UserId, ur.RoleId });
		#endregion

		modelBuilder.Entity<UserModel>()
			.ToTable("Users");

		modelBuilder.Entity<RoleModel>()
			.ToTable("Roles");

		//#region User - Team

		//modelBuilder.Entity<UserModel>()
		//	.HasMany(u => u.Teams)
		//	.WithMany(r => r.Members)
		//	.UsingEntity<UserTeamModel>(
		//		l => l
		//			.HasOne(ut => ut.Team)
		//			.WithMany(t => t.TeamMembers)
		//			.HasForeignKey(ut => ut.TeamId),
		//		p => p
		//			.HasOne(ut => ut.Member)
		//			.WithMany(u => u.TeamMembers)
		//			.HasForeignKey(ut => ut.MemberId));

		//modelBuilder.Entity<UserTeamModel>()
		//	.ToTable("TeamMembers")
		//	.HasKey(ut => new { ut.TeamId, ut.MemberId });

		//modelBuilder.Entity<UserModel>()
		//	.HasMany(u => u.ManagedTeams)
		//	.WithOne(t => t.Lider)
		//	.HasForeignKey(t => t.LiderId);
		//#endregion


		//#region User - Project

		//modelBuilder.Entity<UserModel>()
		//	.HasMany(u => u.OwnedProjects)
		//	.WithMany(r => r.Clients)
		//	.UsingEntity<UserProjectModel>(
		//		l => l
		//			.HasOne(up => up.Project)
		//			.WithMany(t => t.ProjectClients)
		//			.HasForeignKey(ut => ut.ProjectId),
		//		p => p
		//			.HasOne(ut => ut.Owner)
		//			.WithMany(u => u.ProjectClients)
		//			.HasForeignKey(ut => ut.OwnerId));

		//modelBuilder.Entity<UserProjectModel>()
		//	.ToTable("ProjectClients")
		//	.HasKey(pc => new { pc.ProjectId, pc.OwnerId });

		//modelBuilder.Entity<UserModel>()
		//	.HasMany(u => u.ManagedProjects)
		//	.WithOne(p => p.Owner)
		//	.HasForeignKey(p => p.OwnerId);
		//#endregion


		//#region User - TodoList

		//modelBuilder.Entity<UserModel>()
		//	.HasMany(u => u.OwnedTodoLists)
		//	.WithOne(t => t.Owner)
		//	.HasForeignKey(tl => tl.OwnerId);

		//modelBuilder.Entity<UserModel>()
		//	.HasMany(u => u.CreatedTodoLists)
		//	.WithOne(t => t.Creator)
		//	.HasForeignKey(tl => tl.CreatorId);
		//#endregion


		//#region User - Budget

		//modelBuilder.Entity<UserModel>()
		//	.HasMany(u => u.Budgets)
		//	.WithOne(b => b.Owner)
		//	.HasForeignKey(b => b.OwnerId);
		//#endregion


		//#region User - Billings

		//modelBuilder.Entity<UserModel>()
		//	.HasMany(u => u.Billings)
		//	.WithOne(b => b.Executor)
		//	.HasForeignKey(b => b.ExecutorId);
		//#endregion


		//#region User - Incomes

		//modelBuilder.Entity<UserModel>()
		//	.HasMany(u => u.Incomes)
		//	.WithOne(b => b.Executor)
		//	.HasForeignKey(b => b.ExecutorId);
		//#endregion


		_logger?.LogInformation(ExceptionsMessages.LogBuildingSucceed, nameof(OnModelCreating), nameof(CustomIdentityDbContext));
	}
}
