#region USINGS
using App.Features.Billings.Common.Models;
using App.Features.Budgets.Common.Models;
using App.Features.Exceptions.Throw;
using App.Features.Incomes.Common.Models;
using App.Features.Projects.Common.Helpers;
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.Teams.Common.Models;
using App.Features.TodoLists.Common.Models;
using App.Features.TodoLists.Common.Tags;
using App.Features.Users.Common.Roles.Models;
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
	public CustomAppDbContext(DbContextOptions<CustomAppDbContext> options, ILogger<CustomAppDbContext> logger) : base(options)
	{
		_logger = logger;
	}

	public CustomAppDbContext() { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);


		#region Project Entity


		#region Project - User (separate table entity)

			modelBuilder.Entity<ProjectModel>().OwnsMany(
				p => p.ProjectClients, owned =>
				{
					owned.WithOwner().HasForeignKey(up => up.ProjectId);
					owned.Property<Guid>(up => up.Id);
					owned.Property<byte[]>(up => up.RowVersion);
					owned.Property<string>(up => up.OwnerId);
					owned.Property<Guid>(up => up.ProjectId);
					owned.ToTable("UserProjects");

					owned.HasKey(up => up.Id);
				});
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
				.ToTable("ProjectTeams")
				.HasKey(pt => new { pt.ProjectId, pt.TeamId });
		#endregion


		#region Project - Budget

			modelBuilder.Entity<ProjectModel>()
					.HasOne(p => p.Budget)
					.WithOne(b => b.Project);
		#endregion


		#region Project - Tag

			modelBuilder.Entity<ProjectModel>()
				.HasMany(p => p.Tags)
				.WithMany(tg => tg.Projects)
				.UsingEntity<ProjectTagModel>(
				l => l
					.HasOne(pt => pt.Tag)
					.WithMany(p => p.ProjectTags)
					.HasForeignKey(pt => pt.TagId),
				r => r
					.HasOne(pt => pt.Project)
					.WithMany(p => p.ProjectTags)
					.HasForeignKey(pt => pt.ProjectId));

			modelBuilder.Entity<ProjectTagModel>()
				.ToTable("ProjectTags")
				.HasKey(pt => new { pt.ProjectId, pt.TagId });
		#endregion


			modelBuilder.Entity<ProjectModel>()
				.ToTable("Projects");
		#endregion


		#region TodoList Entity


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


		#region TodoList - Tag

			modelBuilder.Entity<TodoListModel>()
				.HasMany(tl => tl.Tags)
				.WithMany(tg => tg.TodoLists)
				.UsingEntity<TodoListTagModel>(
					r => r
						.HasOne(tt => tt.Tag)
						.WithMany(tg => tg.TodoListTags)
						.HasForeignKey(tt => tt.TagId),
					l => l
						.HasOne(tt => tt.TodoList)
						.WithMany(tl => tl.TodoListTags)
						.HasForeignKey(tt => tt.TodoListId));

			modelBuilder.Entity<TodoListTagModel>()
				.ToTable("TodoListTags")
				.HasKey(tlt => new { tlt.TodoListId, tlt.TagId });
		#endregion

			modelBuilder.Entity<TodoListModel>()
				.ToTable("TodoLists");
		#endregion


		#region Task Entity

		
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


		#region Task - Tag

			modelBuilder.Entity<TaskModel>()
				.HasMany(t => t.Tags)
				.WithMany(tg => tg.Tasks)
				.UsingEntity<TaskTagModel>(
					l => l
					.HasOne(tt => tt.Tag)
					.WithMany(tg => tg.TaskTags)
					.HasForeignKey(tt => tt.TagId),
					r => r
					.HasOne(tt => tt.Task)
					.WithMany(t => t.TaskTags)
					.HasForeignKey(tt => tt.TaskId));

			modelBuilder.Entity<TaskTagModel>()
				.ToTable("TaskTags");
		#endregion

			modelBuilder.Entity<TaskModel>()
				.ToTable("Tasks");
		#endregion


		#region Team Entity


		#region Team - User (separate table entity)

			modelBuilder.Entity<TeamModel>().OwnsMany(
				t => t.TeamMembers, owned =>
				{
					owned.WithOwner().HasForeignKey(ut => ut.TeamId);
					owned.Property<Guid>(ut => ut.Id);
					owned.Property<byte[]>(ut => ut.RowVersion);
					owned.Property<string>(ut => ut.MemberId);
					owned.Property<Guid>(ut => ut.TeamId);
					owned.ToTable("UserTeams");

					owned.HasKey(ut => ut.Id);
				});
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
				.ToTable("ProjectTeams")
				.HasKey(pt => new { pt.TeamId, pt.ProjectId });
		#endregion


		#region Team = TodoList

			modelBuilder.Entity<TeamModel>()
					.HasMany(t => t.TodoLists)
					.WithOne(tl => tl.Team)
					.HasForeignKey(tl => tl.TeamId);
		#endregion

			modelBuilder.Entity<TeamModel>()
				.ToTable("Teams");
		#endregion


		#region Budget Entity

		
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


		#region Budget - Tag

			modelBuilder.Entity<BudgetModel>()
				.HasMany(b => b.Tags)
				.WithMany(tg => tg.Budgets)
				.UsingEntity<BudgetTagModel>(
			l => l
				.HasOne(btg => btg.Tag)
				.WithMany(t => t.BudgetTags)
				.HasForeignKey(btg => btg.TagId),
			r => r
				.HasOne(btg => btg.Budget)
				.WithMany(b => b.BudgetTags)
				.HasForeignKey(btg => btg.BudgetId))
			.ToTable("BudgetTags")
			.HasKey(btg => new { btg.BudgetId, btg.TagId });
		#endregion

			modelBuilder.Entity<BudgetModel>()
				.ToTable("Budgets");
		#endregion


		#region Billing Entity

		
		#region Billing - Budget

			modelBuilder.Entity<BillingModel>()
				.HasOne(bl => bl.Budget)
				.WithMany(e => e.Billings)
				.HasForeignKey(bl => bl.BudgetId);
		#endregion

			modelBuilder.Entity<BillingModel>()
				.ToTable("Billings");
		#endregion


		#region Income Entity

		
		#region Income - Budget

			modelBuilder.Entity<IncomeModel>()
				.HasOne(i => i.Budget)
				.WithMany(b => b.Incomes)
				.HasForeignKey(i => i.BudgetId);
		#endregion

			modelBuilder.Entity<IncomeModel>()
				.ToTable("Incomes");
		#endregion


		#region Tag Entity

		#region Tag - Project

			modelBuilder.Entity<TagModel>()
				.HasMany(tg => tg.Projects)
				.WithMany(p => p.Tags)
				.UsingEntity<ProjectTagModel>(
				l => l
					.HasOne(ptg => ptg.Project)
					.WithMany(p => p.ProjectTags)
					.HasForeignKey(ptg => ptg.ProjectId),
				r => r
					.HasOne(ptg => ptg.Tag)
					.WithMany(tg => tg.ProjectTags)
					.HasForeignKey(ptg => ptg.TagId));

			modelBuilder.Entity<ProjectTagModel>()
				.ToTable("ProjectTags")
				.HasKey(ptg => new { ptg.ProjectId, ptg.TagId });
		#endregion


		#region Tag - TodoList

			modelBuilder.Entity<TagModel>()
				.HasMany(tg => tg.TodoLists)
				.WithMany(tl => tl.Tags)
				.UsingEntity<TodoListTagModel>(
				l => l
					.HasOne(tltg => tltg.TodoList)
					.WithMany(tl => tl.TodoListTags)
					.HasForeignKey(tltg => tltg.TodoListId),
				r => r
					.HasOne(tltg => tltg.Tag)
					.WithMany(tg => tg.TodoListTags)
					.HasForeignKey(tltg => tltg.TagId));

			modelBuilder.Entity<TodoListTagModel>()
				.ToTable("TodoListTags")
				.HasKey(tltg => new { tltg.TodoListId, tltg.TagId });
		#endregion


		#region Tag - Task

			modelBuilder.Entity<TagModel>()
				.HasMany(tg => tg.Tasks)
				.WithMany(tl => tl.Tags)
				.UsingEntity<TaskTagModel>(
				l => l
					.HasOne(ttg => ttg.Task)
					.WithMany(t => t.TaskTags)
					.HasForeignKey(ttg => ttg.TaskId),
				r => r
					.HasOne(ttg => ttg.Tag)
					.WithMany(tg => tg.TaskTags)
					.HasForeignKey(ttg => ttg.TagId));

			modelBuilder.Entity<TaskTagModel>()
				.ToTable("TaskTags")
				.HasKey(ttg => new { ttg.TaskId, ttg.TagId });
		#endregion


		#region Tag - Budget

			modelBuilder.Entity<TagModel>()
				.HasMany(tg => tg.Budgets)
				.WithMany(tl => tl.Tags)
				.UsingEntity<BudgetTagModel>(
			l => l
				.HasOne(btg => btg.Budget)
				.WithMany(b => b.BudgetTags)
				.HasForeignKey(btg => btg.BudgetId),
			r => r
				.HasOne(btg => btg.Tag)
				.WithMany(tg => tg.BudgetTags)
				.HasForeignKey(btg => btg.TagId))
			.ToTable("BudgetTags")
			.HasKey(btg => new { btg.BudgetId, btg.TagId });
		#endregion

			modelBuilder.Entity<TagModel>()
				.ToTable("Tags");
		#endregion


		_logger?.LogInformation(ExceptionsMessages.LogBuildingSucceed, nameof(CustomAppDbContext), nameof(OnModelCreating));
	}
}
