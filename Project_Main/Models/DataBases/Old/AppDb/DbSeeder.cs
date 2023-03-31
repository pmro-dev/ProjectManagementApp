using Project_Main.Models.DataBases.Repositories.AppData;

namespace Project_Main.Models.DataBases.Old.AppDb
{
	/// <summary>
	/// Class that manage seeding data to database.
	/// </summary>
	public static class DbSeeder
	{
		/// <summary>
		/// Checks that Database is set and populated, if not, try to create database, applies migrations and seed data to it.
		/// </summary>
		/// <param name="app">Application builder.</param>
		public static async Task EnsurePopulated(IApplicationBuilder app, ILogger logger)
		{
			//AppDbContext context = app.ApplicationServices
			//			.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

			DataUnitOfWork _unitOfWork = app.ApplicationServices
						.CreateScope().ServiceProvider.GetRequiredService<DataUnitOfWork>();

			SeedData seedContainer = app.ApplicationServices
						.CreateScope().ServiceProvider.GetRequiredService<SeedData>();

			//string errorPrefixMessage = nameof(DbSeeder) + " | Populating Database ";
			//context.CheckAllDbSetsIfAnyNullThrowException(errorPrefixMessage);

			using var transaction = _unitOfWork.BeginTransactionAsync();
			//using var transaction = await context.Database.BeginTransactionAsync();

			try
			{
				var migrations = await _unitOfWork.GetPendingMigrationsAsync();

				if (migrations.Any())
				{
					//await context.Database.MigrateAsync();
					await _unitOfWork.MigrateAsync();
				}

				ITodoListRepository todoListRepository = _unitOfWork.TodoListRepository;

				//if (!await context.TodoLists.AnyAsync())
				if (!await todoListRepository.ContainsAny())
				{
					await todoListRepository.AddRangeAsync(seedContainer.TodoLists);
					//await context.TodoLists.AddRangeAsync(seedContainer.TodoLists);
					//await context.SaveChangesAsync();
				}

				ITaskRepository taskRepository = _unitOfWork.TaskRepository;

				if (!await taskRepository.ContainsAny())
				{
					await taskRepository.AddRangeAsync(seedContainer.AllTasks);

					//await context.Tasks.AddRangeAsync(seedContainer.AllTasks);
					//await context.SaveChangesAsync();
				}

				await _unitOfWork.SaveChangesAsync();
				await _unitOfWork.CommitTransactionAsync();	

				//await transaction.CommitAsync();
			}
			catch (Exception ex)
			{
				logger.LogCritical(ex, "Populating Database | Critical Error! Couldn't finish transactions -> Add range to To Do Lists DbSet and Tasks DbSet.");
				throw;
			}
		}
	}
}


