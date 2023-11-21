﻿using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Databases.App.Seeds.Interfaces;

namespace App.Infrastructure.Databases.App.Seeds;

/// <summary>
/// Class manages seeding main data to database.
/// </summary>
public static class DbSeeder
{
	/// <summary>
	/// Checks that Database is set and populated, if not -> try to create database, applies migrations and seed data to it.
	/// </summary>
	/// <param name="app">Application builder.</param>
	public static async Task EnsurePopulated(IDataUnitOfWork unitOfWork, ISeedData seedContainer, ILogger logger)
	{
		using var transaction = await unitOfWork.BeginTransactionAsync();

        try
		{
			await transaction.CreateSavepointAsync("BeforeMigrations");
			await EnsurePendingMigrationsAppliedAsync(unitOfWork);

            await transaction.CreateSavepointAsync("BeforeRolesAndAdminPopulated");
            await EnsureTodoListsPopulatedAsync(seedContainer, unitOfWork);
			await EnsureTasksPopulatedAsync(seedContainer, unitOfWork);

			await unitOfWork.SaveChangesAsync();
			await unitOfWork.CommitTransactionAsync();
		}
		catch (Exception ex)
		{
			await unitOfWork.RollbackTransactionAsync();
			logger.LogCritical(ex, "An error occurred while populating the App database.");
			throw;
		}
	}

	private static async Task EnsurePendingMigrationsAppliedAsync(IDataUnitOfWork unitOfWork)
	{
		var migrations = await unitOfWork.GetPendingMigrationsAsync();

		if (migrations.Any())
		{
			await unitOfWork.MigrateAsync();
		}
	}

	private static async Task EnsureTodoListsPopulatedAsync(ISeedData seedContainer, IDataUnitOfWork unitOfWork)
	{
		ITodoListRepository todoListRepository = unitOfWork.TodoListRepository;

		if (!await todoListRepository.ContainsAny())
		{
			await todoListRepository.AddRangeAsync(seedContainer.TodoLists);
		}
	}

	private static async Task EnsureTasksPopulatedAsync(ISeedData seedContainer, IDataUnitOfWork unitOfWork)
	{
		ITaskRepository taskRepository = unitOfWork.TaskRepository;

		if (!await taskRepository.ContainsAny())
		{
			await taskRepository.AddRangeAsync(seedContainer.AllTasks);
		}
	}
}


