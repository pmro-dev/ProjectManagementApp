using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Databases.App.Seeds.Interfaces;

namespace App.Infrastructure.Databases.App.Seeds;

/// <summary>
/// Class manages seeding main data to database.
/// </summary>
public class DbSeeder : IDbSeeder
{
	private readonly ISeedData _seedContainer;
	private readonly IDataUnitOfWork _unitOfWork;
	private readonly ILogger<DbSeeder> _logger;

	public DbSeeder(ISeedData seedContainer, IDataUnitOfWork unitOfWork, ILogger<DbSeeder> logger)
	{
		_seedContainer = seedContainer;
		_unitOfWork = unitOfWork;
		_logger = logger;
	}

	/// <summary>
	/// Checks that Database is set and populated, if not -> try to create database, applies migrations and seed data to it.
	/// </summary>
	/// <param name="app">Application builder.</param>
	public async Task EnsurePopulated()
	{
		using var transaction = await _unitOfWork.BeginTransactionAsync();

		try
		{
			await transaction.CreateSavepointAsync("BeforeMigrations");
			await EnsurePendingMigrationsAppliedAsync();

			await transaction.CreateSavepointAsync("BeforeRolesAndAdminPopulated");
			await EnsureTodoListsPopulatedAsync();
			await EnsureTasksPopulatedAsync();

			await _unitOfWork.SaveChangesAsync();
			await _unitOfWork.CommitTransactionAsync();
		}
		catch (Exception ex)
		{
			await _unitOfWork.RollbackTransactionAsync();
			_logger.LogCritical(ex, "An error occurred while populating the App database.");
			throw;
		}
	}

	private async Task EnsurePendingMigrationsAppliedAsync()
	{
		var migrations = await _unitOfWork.GetPendingMigrationsAsync();

		if (migrations.Any())
		{
			await _unitOfWork.MigrateAsync();
		}
	}

	private async Task EnsureTodoListsPopulatedAsync()
	{
		ITodoListRepository todoListRepository = _unitOfWork.TodoListRepository;

		if (!await todoListRepository.ContainsAnyAsync())
		{
			await todoListRepository.AddRangeAsync(_seedContainer.TodoLists);
		}
	}

	private async Task EnsureTasksPopulatedAsync()
	{
		ITaskRepository taskRepository = _unitOfWork.TaskRepository;

		if (!await taskRepository.ContainsAnyAsync())
		{
			await taskRepository.AddRangeAsync(_seedContainer.AllTasks);
		}
	}
}


