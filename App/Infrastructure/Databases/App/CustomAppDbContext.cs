#region USINGS
using App.Features.Billings.Common.Models;
using App.Features.Budgets.Common.Models;
using App.Features.Exceptions.Throw;
using App.Features.Incomes.Common.Models;
using App.Features.Projects.Common.Helpers;
using App.Features.Projects.Common.Models;
using App.Features.Tasks.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Projects.Models;
using App.Features.Users.Common.Teams.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;
#endregion

namespace App.Infrastructure.Databases.App;

/// <summary>
/// Custom Db Context for main app data.
/// </summary>
public class CustomAppDbContext : DbContext
{
	private readonly ILogger<CustomAppDbContext>? _logger;

	/// <summary>
	/// Initilizes object and Ensures that database is created.
	/// </summary>
	public CustomAppDbContext(DbContextOptions<CustomAppDbContext> options, ILogger<CustomAppDbContext> logger)
		: base(options) { _logger = logger; }

	public CustomAppDbContext() { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);


		#region Project Entity

			#region Project - User

			modelBuilder.Entity<ProjectModel>()
				.HasOne(p => p.Owner)
				.WithMany(o => o.Projects)
				.HasForeignKey(p => p.OwnerId);

			modelBuilder.Entity<ProjectModel>()
				.HasMany(p => p.Clients)
				.WithMany(o => o.Projects)
				.UsingEntity<UserProjectModel>(
					l => l
						.HasOne(up => up.Owner)
						.WithMany(o => o.ClientProjects)
						.HasForeignKey(up => up.OwnerId),
					r => r
						.HasOne(up => up.Project)
						.WithMany(p => p.ProjectClients)
						.HasForeignKey(up => up.ProjectId));

		modelBuilder.Entity<UserProjectModel>()
			.ToTable("ProjectClients");

		#endregion


			#region Project - Status

			modelBuilder.Entity<ProjectModel>()
				.Property(x => x.Status)
				.HasConversion(new EnumToStringConverter<ProjectStatusType>());
			#endregion


			#region Project - TodoList

			modelBuilder.Entity<ProjectModel>()
				.HasMany(p => p.TodoLists)
				.WithOne(tl => tl.Project)
				.HasForeignKey(tl => tl.ProjectId);
			#endregion


			#region Project - Team

			modelBuilder.Entity<ProjectModel>()
				.HasMany(p => p.Teams)
				.WithMany(t => t.Projects)
				.UsingEntity<ProjectTeamModel>(
					l => l
						.HasOne(pt => pt.Team)
						.WithMany(p => p.TeamProjects)
						.HasForeignKey(pt => pt.TeamId),
					r => r
						.HasOne(pt => pt.Project)
						.WithMany(p => p.ProjectTeams)
						.HasForeignKey(pt => pt.ProjectId));

			modelBuilder.Entity<ProjectTeamModel>()
				.ToTable("ProjectTeams");
			#endregion


			#region Project - Budget

			modelBuilder.Entity<ProjectModel>()
					.HasOne(p => p.Budget)
					.WithOne(b => b.Project)
					.HasForeignKey<BudgetModel>();
			#endregion

		#endregion


		#region TodoList Entity

			#region TodoList - User 

			modelBuilder.Entity<TodoListModel>()
				.HasOne(tl => tl.Owner)
				.WithMany(o => o.TodoLists)
				.HasForeignKey(tl => tl.OwnerId);

			modelBuilder.Entity<TodoListModel>()
				.HasOne(tl => tl.Creator)
				.WithMany(c => c.TodoLists)
				.HasForeignKey(tl => tl.CreatorId);
			#endregion


			#region TodoList - Task

			modelBuilder.Entity<TodoListModel>()
				.HasMany(tl => tl.Tasks)
				.WithOne(t => t.TodoList)
				.HasForeignKey(t => t.TodoListId);
			#endregion


			#region TodoList - Team 

			modelBuilder.Entity<TodoListModel>()
				.HasOne(tl => tl.Team)
				.WithMany(t => t.TodoLists)
				.HasForeignKey(tl => tl.TeamId);
			#endregion


			#region TodoList - Project 

			modelBuilder.Entity<TodoListModel>()
				.HasOne(tl => tl.Project)
				.WithMany(p => p.TodoLists)
				.HasForeignKey(tl => tl.ProjectId);
			#endregion


			modelBuilder.Entity<TodoListModel>()
				.ToTable("TodoLists")
				.HasKey(t => t.Id);

		#endregion


		#region Task Entity

			#region Task - User

			modelBuilder.Entity<TaskModel>()
				.HasOne(t => t.Owner)
				.WithMany(o => o.Tasks)
				.HasForeignKey(t => t.OwnerId);
			#endregion


			#region Task - Status

			modelBuilder.Entity<TaskModel>()
				.Property(x => x.Status)
				.HasConversion(new EnumToStringConverter<TaskStatusType>());
			#endregion


			#region Task - TodoList

			modelBuilder.Entity<TaskModel>()
				.HasOne(t => t.TodoList)
				.WithMany(tl => tl.Tasks)
				.HasForeignKey(t => t.TodoListId);
			#endregion

		#endregion


		#region Team Entity

			#region Team - User

			modelBuilder.Entity<TeamModel>()
				.HasOne(t => t.Lider)
				.WithMany(l => l.Teams)
				.HasForeignKey(t => t.LiderId);

			modelBuilder.Entity<TeamModel>()
				.HasMany(t => t.Members)
				.WithMany(m => m.Teams)
				.UsingEntity<UserTeamModel>(
					l => l
						.HasOne(ut => ut.Member)
						.WithMany(m => m.UserTeams)
						.HasForeignKey(ut => ut.MemberId),
					r => r
						.HasOne(ut => ut.Team)
						.WithMany(t => t.TeamMembers)
						.HasForeignKey(tm => tm.TeamId));

			modelBuilder.Entity<UserTeamModel>()
				.ToTable("UserTeams");
		#endregion

			#region Team - Project

			modelBuilder.Entity<TeamModel>()
					.HasMany(t => t.Projects)
					.WithMany(p => p.Teams)
					.UsingEntity<ProjectTeamModel>(
						l => l
							.HasOne(pt => pt.Project)
							.WithMany(t => t.ProjectTeams)
							.HasForeignKey(tp => tp.ProjectId),
						r => r
							.HasOne(pt => pt.Team)
							.WithMany(p => p.TeamProjects)
							.HasForeignKey(pt => pt.TeamId));

			modelBuilder.Entity<ProjectTeamModel>()
				.ToTable("ProjectTeams");
		#endregion

			#region Team = TodoList

			modelBuilder.Entity<TeamModel>()
					.HasMany(t => t.TodoLists)
					.WithOne(tl => tl.Team)
					.HasForeignKey(tl => tl.TeamId);
			#endregion

		#endregion


		#region Budget Entity

			#region Budget - User

			modelBuilder.Entity<BudgetModel>()
				.HasOne(b => b.Owner)
				.WithMany(o => o.Budgets)
				.HasForeignKey(o => o.OwnerId);
		#endregion


			#region Budget - Project

			modelBuilder.Entity<BudgetModel>()
				.HasOne(b => b.Project)
				.WithOne(p => p.Budget);
		#endregion


			#region Budget - Billings

			modelBuilder.Entity<BudgetModel>()
				.HasMany(b => b.Billings)
				.WithOne(bl => bl.Budget)
				.HasForeignKey(b => b.BudgetId);
		#endregion


			#region Budget - Incomes

			modelBuilder.Entity<BudgetModel>()
				.HasMany(b => b.Incomes)
				.WithOne(i => i.Budget)
				.HasForeignKey(b => b.BudgetId);
		#endregion

		#endregion


		#region Billing Entity

			#region Billing - User

			modelBuilder.Entity<BillingModel>()
				.HasOne(bl => bl.Executor)
				.WithMany(e => e.Billings)
				.HasForeignKey(bl => bl.ExecutorId);
		#endregion

			#region Billing - Budget

			modelBuilder.Entity<BillingModel>()
				.HasOne(bl => bl.Budget)
				.WithMany(e => e.Billings)
				.HasForeignKey(bl => bl.BudgetId);
		#endregion

		#endregion


		#region Income Entity

			#region Income - User

			modelBuilder.Entity<IncomeModel>()
				.HasOne(i => i.Executor)
				.WithMany(e => e.Incomes)
				.HasForeignKey(i => i.ExecutorId);
			#endregion

			#region Income - Budget

			modelBuilder.Entity<IncomeModel>()
				.HasOne(i => i.Budget)
				.WithMany(b => b.Incomes)
				.HasForeignKey(i => i.BudgetId);
			#endregion

		#endregion


		_logger?.LogInformation(ExceptionsMessages.LogBuildingSucceed, nameof(CustomAppDbContext), nameof(OnModelCreating));
	}
}
