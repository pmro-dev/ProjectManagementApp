using Microsoft.EntityFrameworkCore;

namespace TODO_List_ASPNET_MVC.Models.DataBase
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
        public static void EnsurePopulated(IApplicationBuilder app, ILogger logger)
        {
            AppDbContext context = app.ApplicationServices
                        .CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

            SeedData seedContainer = app.ApplicationServices
                        .CreateScope().ServiceProvider.GetRequiredService<SeedData>();

            string errorPrefixMessage = nameof(DbSeeder) + " | Populating Database ";
			context.CheckAllDbSetsIfAnyNullThrowException(errorPrefixMessage);
            
            using var transaction = context.Database.BeginTransaction();

            try
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.TodoLists.Any())
                {
                    context.TodoLists.AddRange(seedContainer.TodoLists);

                    context.SaveChanges();
                }

                if (!context.Tasks.Any())
                {
                    context.Tasks.AddRange(seedContainer.AllTasks);

                    context.SaveChanges();
                }

                transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Populating Database | Critical Error! Couldn't finish transactions -> Add range to To Do Lists DbSet and Tasks DbSet.");
				throw;
            }
        }
    }
}


