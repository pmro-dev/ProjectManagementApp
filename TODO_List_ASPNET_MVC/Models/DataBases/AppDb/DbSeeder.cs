using Microsoft.EntityFrameworkCore;

namespace TODO_List_ASPNET_MVC.Models.DataBases.AppDb
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
			AppDbContext context = app.ApplicationServices
						.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

			SeedData seedContainer = app.ApplicationServices
						.CreateScope().ServiceProvider.GetRequiredService<SeedData>();

			string errorPrefixMessage = nameof(DbSeeder) + " | Populating Database ";
			context.CheckAllDbSetsIfAnyNullThrowException(errorPrefixMessage);

			using var transaction = await context.Database.BeginTransactionAsync();

			try
			{
				var migrations = await context.Database.GetPendingMigrationsAsync();

				if (migrations.Any())
				{
					await context.Database.MigrateAsync();
				}

				if (!await context.TodoLists.AnyAsync())
				{
					await context.TodoLists.AddRangeAsync(seedContainer.TodoLists);
					await context.SaveChangesAsync();
				}

				if (!await context.Tasks.AnyAsync())
				{
					await context.Tasks.AddRangeAsync(seedContainer.AllTasks);
					await context.SaveChangesAsync();
				}

				await transaction.CommitAsync();
			}
			catch (Exception ex)
			{
				logger.LogCritical(ex, "Populating Database | Critical Error! Couldn't finish transactions -> Add range to To Do Lists DbSet and Tasks DbSet.");
				throw;
			}
		}
	}
}


