#region USINGS
using App.Features.Exceptions.Throw;
using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.TodoLists.Common.Models;
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

		modelBuilder.Entity<TodoListModel>()
			.HasMany(list => list.Tasks)
			.WithOne()
			.HasForeignKey(x => x.Id);

		modelBuilder.Entity<TodoListModel>()
			.ToTable("TodoLists")
			.HasKey(t => t.Id);

		modelBuilder.Entity<TaskModel>()
			.HasMany(task => task.TaskTags)
			.WithOne()
			.HasForeignKey(x => x.TaskId)
			.HasForeignKey(x => x.TagId);

		modelBuilder.Entity<TaskModel>()
			.HasOne(task => task.TodoList)
			.WithMany(x => x.Tasks)
			.HasForeignKey(x => x.TodoListId);

		modelBuilder.Entity<TaskModel>()
			.ToTable("Tasks").HasKey(t => t.Id);

		modelBuilder.Entity<TagModel>()
			.HasMany(tag => tag.TaskTags)
			.WithOne(x => x.Tag)
			.HasForeignKey(x => x.TagId)
			.HasForeignKey(x => x.TaskId);

		modelBuilder.Entity<TagModel>()
			.ToTable("Tags").HasKey(t => t.Id);

		modelBuilder.Entity<TaskTagModel>()
			.HasOne(taskTag => taskTag.Task)
			.WithMany(x => x.TaskTags)
			.HasForeignKey(x => x.TaskId);

		modelBuilder.Entity<TaskTagModel>()
			.HasOne(taskTag => taskTag.Tag)
			.WithMany(x => x.TaskTags)
			.HasForeignKey(x => x.TagId);

		modelBuilder.Entity<TaskTagModel>()
			.ToTable("TaskTags").HasKey("TaskId", "TagId");

		var todoItemBuilder = modelBuilder.Entity<TaskModel>();
		todoItemBuilder.Property(x => x.Status)
			.HasConversion(new EnumToStringConverter<TaskStatusType>());

		_logger?.LogInformation(ExceptionsMessages.LogBuildingSucceed, nameof(CustomAppDbContext), nameof(OnModelCreating));
	}
}
