using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TODO_List_ASPNET_MVC.Models.ViewModels
{
	/// <summary>
	/// Class that manage seeding Identity data to database.
	/// </summary>
	public static class IdentitySeedData
	{
		private const string AdminUser = "Admin";
		private const string AdminPassword = "Secret123$";

		/// <summary>
		/// Checks that Identity Database is set and populated, if not, try to create database, applies migrations and seed data to it.
		/// </summary>
		/// <param name="app">Application builder.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Bug", "S3168:\"async\" methods should not return \"void\"", Justification = "<NOTHING>")]
		public static async void EnsurePopulated(IApplicationBuilder app, ILogger logger)
		{
			AppIdentityDbContext context = app.ApplicationServices
				.CreateScope().ServiceProvider
				.GetRequiredService<AppIdentityDbContext>();

			try
			{
				if (context.Database.GetPendingMigrations().Any())
				{
					context.Database.Migrate();
				}

				UserManager<IdentityUser> userManager = app.ApplicationServices
					.CreateScope().ServiceProvider
					.GetRequiredService<UserManager<IdentityUser>>();

				IdentityUser user = await userManager.FindByNameAsync(AdminUser);

				if (user is null)
				{
					user = new IdentityUser("Admin")
					{
						Email = "admin@todolist.com",
						PhoneNumber = "123-456",
						Id = "adminId"
					};

					await userManager.CreateAsync(user, AdminPassword);
				}
			}
			catch (Exception ex)
			{
				logger.LogCritical(ex, "Populating Database | Critical Error! Couldn't finish transactions -> Add range to To Do Lists DbSet and Tasks DbSet.");
				throw;
			}
		}
	}
}
