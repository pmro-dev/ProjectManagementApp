using Identity_Domain_Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace TODO_List_ASPNET_MVC.Models.DataBases.IdentityDb
{
	/// <summary>
	/// Class that manage seeding Identity data to database.
	/// </summary>
	public static class IdentitySeedData
	{
		private const string AdminUser = "Admin";
		private const string AdminId = "adminId";
		private const string AdminPassword = "Secret123$";
		private const string AdminEmail = "admin@gmail.com";
		private const string ProviderDbSeederName = CookieAuthenticationDefaults.AuthenticationScheme;

		private static Dictionary<string, string> BasicRoles { get; set; } = new Dictionary<string, string>()
		{
			{ "Guest", "Guest has access only to certain, public places for a short period of time." },
			{ "Developer", "Developer has access to a project environment (including team area)" },
			{ "ScrumMaster", "ScrumMaster inherits access from Developer and exceed it by an area with basic, statistic data of a project progression." },
			{ "TeamLeader", "Team Leader inherits access from Developer and exceed it by tech discussion area and shares it with some other roles." },
			{ "ProjectOwner", "Project Owner as Project Manager gas access to project statistics and data, but cannot to manage a project." },
			{ "ProjectManager", "Project Manager has the most wide access to project statistics and data. Can manage projects, teams, tasks and sprints." },
			{ "Analyst", "Analyst has access to a specific module with statistics details and project plans." }
		};

		/// <summary>
		/// Checks that Identity Database is set and populated, if not, try to create database, applies migrations and seed data to it.
		/// </summary>
		/// <param name="app">Application builder.</param>
		public static async Task EnsurePopulated(IApplicationBuilder app, ILogger logger)
		{
			SetupServices(app, out IdentityDbContext context);

			using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();

			try
			{
				await EnsurePendingMigrationsApplied(context);
				await EnsureRolesPopulated(context);
				await EnsureAdminPopulated(context);
				await transaction.CommitAsync();
			}
			catch (Exception ex)
			{
				logger.LogCritical(ex, "Populating Identity Database | Critical Error! Could not to finish transaction.");
				throw;
			}
		}

		private static void SetupServices(IApplicationBuilder app, out IdentityDbContext context)
		{
			 context = app.ApplicationServices
				.CreateScope().ServiceProvider
				.GetRequiredService<IdentityDbContext>();
		}

		private static async Task EnsurePendingMigrationsApplied(IdentityDbContext context)
		{
			if (context.Database.GetPendingMigrations().Any())
			{
				await context.Database.MigrateAsync();
			}
		}

		private static async Task EnsureRolesPopulated(IdentityDbContext context)
		{
			if (!await context.Roles.AnyAsync())
			{
				List<RoleModel> defaultRoles = new();

				foreach (KeyValuePair<string, string> pair in BasicRoles)
				{
					defaultRoles.Add(new RoleModel()
					{
						Name = pair.Key,
						Description = pair.Value
					});
				}

				await context.Roles.AddRangeAsync(defaultRoles);
				await context.SaveChangesAsync();
			}
		}

		private static async Task EnsureAdminPopulated(IdentityDbContext context)
		{
			if (!await context.Users.AnyAsync() || !await context.Users.AnyAsync(u => u.UserId == AdminId))
			{
				UserModel user = new()
				{
					UserId = AdminId,
					FirstName = AdminUser,
					Lastname = AdminUser,
					NameIdentifier = AdminId,
					Password = AdminPassword,
					Provider = ProviderDbSeederName,
					Username = AdminUser,
					Email = AdminEmail
				};

				RoleModel role = await context.Roles.SingleAsync(r => r.Name == BasicRoles.Keys.Last());

				user.UserRoles.Add(new UserRoleModel() { Role = role, User = user });

				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();
			}
		}
	}
}
