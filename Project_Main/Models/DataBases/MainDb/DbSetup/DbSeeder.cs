using ClassLibrary_SeedData;
using Microsoft.Extensions.DependencyInjection;

namespace Project_Main.Models.DataBases.AppData.DbSetup
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
            IDataUnitOfWork _unitOfWork = app.ApplicationServices
                        .CreateScope().ServiceProvider.GetRequiredService<IDataUnitOfWork>();

            ISeedData seedContainer = app.ApplicationServices
                        .CreateScope().ServiceProvider.GetRequiredService<ISeedData>();

            using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var migrations = await _unitOfWork.GetPendingMigrationsAsync();

                if (migrations.Any())
                {
                    await _unitOfWork.MigrateAsync();
                }

                ITodoListRepository todoListRepository = _unitOfWork.TodoListRepository;

                if (!await todoListRepository.ContainsAny())
                {
                    await todoListRepository.AddRangeAsync(seedContainer.TodoLists);
                }

                ITaskRepository taskRepository = _unitOfWork.TaskRepository;

                if (!await taskRepository.ContainsAny())
                {
                    await taskRepository.AddRangeAsync(seedContainer.AllTasks);
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                logger.LogCritical(ex, "An error occurred while populating the database.");
                throw;
            }
        }
    }
}


