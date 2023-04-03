using Project_IdentityDomainEntities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Project_Main.Models.DataBases.Repositories.Identity;

namespace Project_Main.Models.DataBases.Old.IdentityDb
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

		//private static IIdentityUnitOfWork? identityUnitOfWork;

		/// <summary>
		/// Checks that Identity Database is set and populated, if not, try to create database, applies migrations and seed data to it.
		/// </summary>
		/// <param name="app">Application builder.</param>
		public static async Task EnsurePopulated(IApplicationBuilder app, ILogger logger)
		{
			IIdentityUnitOfWork identityUnitOfWork = app.ApplicationServices
				.CreateScope().ServiceProvider.GetRequiredService<IIdentityUnitOfWork>();

			using var transaction = await identityUnitOfWork.BeginTransactionAsync();

			try
			{
				await EnsurePendingMigrationsAppliedAsync(identityUnitOfWork);
				await EnsureRolesPopulatedAsync(identityUnitOfWork);
				await EnsureAdminPopulatedAsync(identityUnitOfWork);

				await identityUnitOfWork.SaveChangesAsync();
				identityUnitOfWork.CommitTransaction();
				await identityUnitOfWork.CommitTransactionAsync();
			}
			catch (Exception ex)
			{
				await identityUnitOfWork.RollbackTransactionAsync();
				logger.LogError(ex, "An error occurred while populating the database.");
				throw;
			}
			}

		private static async Task EnsurePendingMigrationsAppliedAsync(IIdentityUnitOfWork identityUnitOfWork)
		{
			var migrations = await identityUnitOfWork.GetPendingMigrationsAsync();

			if (migrations.Any())
			{
				await identityUnitOfWork.MigrateAsync();
			}
		}

		private static async Task EnsureRolesPopulated(IIdentityUnitOfWork identityUnitOfWork)
		private static async Task EnsureRolesPopulatedAsync(IIdentityUnitOfWork identityUnitOfWork)
		{
			IRoleRepository roleRepository = identityUnitOfWork.RoleRepository;

			if (!await roleRepository.ContainsAny())
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

				await roleRepository.AddRangeAsync(defaultRoles);
				await identityUnitOfWork.SaveChangesAsync();
			}
		}

		private static async Task EnsureAdminPopulatedAsync(IIdentityUnitOfWork identityUnitOfWork)
		{
			IUserRepository userRepository = identityUnitOfWork.UserRepository;
			IRoleRepository roleRepository = identityUnitOfWork.RoleRepository;

			if (!await userRepository.ContainsAny())
			{
				IEnumerable<RoleModel> filteredRoles = await roleRepository.GetByFilterAsync(r => r.Name == BasicRoles.Keys.Last());
				RoleModel roleTemp = filteredRoles.First();

				UserModel user = new()
				{
					UserId = AdminId,
					FirstName = AdminUser,
					Lastname = AdminUser,
					NameIdentifier = AdminId,
					Password = AdminPassword,
					Provider = ProviderDbSeederName,
					Username = AdminUser,
					Email = AdminEmail,
					UserRoles = new List<UserRoleModel>() { new UserRoleModel() { RoleId = roleTemp.Id, UserId = AdminId } }
				};
				
			//RoleModel role = await context.Roles.SingleAsync(r => r.Name == BasicRoles.Keys.Last());

				//user.UserRoles.Add(new UserRoleModel() { Role = role, User = user });

				await userRepository.AddAsync(user);
				await identityUnitOfWork.SaveChangesAsync();

				//await context.Users.AddAsync(user);
				//await context.SaveChangesAsync();
			}
		}
	}
}
