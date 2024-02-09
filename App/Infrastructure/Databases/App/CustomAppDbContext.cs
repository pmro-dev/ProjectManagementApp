#region USINGS
using App.Features.Budgets.Common.Models;
using App.Features.Exceptions.Throw;
using App.Features.Projects.Common.Helpers;
using App.Features.Projects.Common.Models;
using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Projects.Models;
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


		#region TodoList - Task

		modelBuilder.Entity<TodoListModel>()
			.HasMany(tl => tl.Tasks)
			.WithOne(t => t.TodoList)
			.HasForeignKey(t => t.TodoListId);
		#endregion


		#region TodoList - Project 

		modelBuilder.Entity<TodoListModel>()
			.HasOne(tl => tl.Project)
			.WithMany(p => p.TodoLists)
			.HasForeignKey(tl => tl.ProjectId);
		#endregion


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


		#region TodoList - Team 

		modelBuilder.Entity<TodoListModel>()
			.HasOne(tl => tl.Team)
			.WithMany(t => t.TodoLists)
			.HasForeignKey(tl => tl.TeamId);
		#endregion


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
				.HasForeignKey(up => up.ProjectId)
			)
			.ToTable("ProjectClients");

		#endregion


		#region Project - Status

		var projectStatusBuilder = modelBuilder.Entity<ProjectModel>();
		projectStatusBuilder.Property(x => x.Status)
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
				.HasForeignKey(pt => pt.ProjectId)
			)
			.ToTable("ProjectTeams");
		#endregion


		#region Project - Budget

		modelBuilder.Entity<ProjectModel>()
			.HasOne(p => p.Budget)
			.WithOne(b => b.Project)
			.HasForeignKey<BudgetModel>();
		#endregion


		modelBuilder.Entity<TodoListModel>()
			.ToTable("TodoLists")
			.HasKey(t => t.Id);


		var todoItemBuilder = modelBuilder.Entity<TaskModel>();
		todoItemBuilder.Property(x => x.Status)
			.HasConversion(new EnumToStringConverter<TaskStatusType>());

		_logger?.LogInformation(ExceptionsMessages.LogBuildingSucceed, nameof(CustomAppDbContext), nameof(OnModelCreating));
	}
}
