using App.Features.Users.Authentication;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Models;
using App.Infrastructure.Databases.Identity.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace App.Infrastructure.Databases.Identity.Seeds;

/// <summary>
/// Class manages seeding Identity data to database.
/// </summary>
public class IdentityDbSeeder : IIdentityDbSeeder
{
	public const string DefaultRole = "Guest";
	private const string RoleIdSuffix = "RoleId";
	private const string AdminUser = "Admin";
	private const string AdminRoleName = "Admin";
	private const string AdminId = "adminId";
	private const string AdminPassword = "Secret123$";
	private const string AdminEmail = "admin@gmail.com";
	private const string ProviderName = AuthenticationConsts.DefaultScheme;
	private readonly ILogger<IdentityDbSeeder> _logger;
	private readonly IIdentityUnitOfWork _identityUnitOfWork;

	public IdentityDbSeeder(ILogger<IdentityDbSeeder> logger, IIdentityUnitOfWork identityUnitOfWork)
	{
		_logger = logger;
		_identityUnitOfWork = identityUnitOfWork;
	}

	private static readonly UserModel AdminInitModel = new()
	{
		Id = AdminId,
		FirstName = AdminUser,
		LastName = AdminUser,
		NameIdentifier = AdminId,
		Password = AdminPassword,
		Provider = ProviderName,
		Username = AdminUser,
		Email = AdminEmail
	};

	private static Dictionary<string, string> BasicRoles { get; set; } = new Dictionary<string, string>()
	{
		{ AdminRoleName, "Admin may do everything!"},
		{ DefaultRole, "Guest has access only to certain, public places for a short period of time." },
		{ "Developer", "Developer has access to a project environment (including team area)" },
		{ "ScrumMaster", "ScrumMaster inherits access from Developer and exceed it by an area with basic, statistic data of a project progression." },
		{ "TeamLeader", "Team Leader inherits access from Developer and exceed it by tech discussion area and shares it with some other roles." },
		{ "ProjectOwner", "Project Owner as Project Manager gas access to project statistics and data, but cannot to manage a project." },
		{ "ProjectManager", "Project Manager has the most wide access to project statistics and data. Can manage projects, teams, tasks and sprints." },
		{ "Analyst", "Analyst has access to a specific module with statistics details and project plans." }
	};

	/// <summary>
	/// Checks that Identity Database is set and populated, if not -> try to create database, applies migrations and seed data to it.
	/// </summary>
	/// <param name="app">Application builder.</param>
	public async Task EnsurePopulated()
	{
		using var transaction = await _identityUnitOfWork.BeginTransactionAsync();

		try
		{
			await transaction.CreateSavepointAsync("BeforeMigrations");
			await EnsurePendingMigrationsAppliedAsync();

			await transaction.CreateSavepointAsync("BeforeRolesAndAdminPopulated");
			await EnsureRolesPopulatedAsync();
			await EnsureAdminPopulatedAsync();
			await _identityUnitOfWork.SaveChangesAsync();

			await transaction.CreateSavepointAsync("BeforeRoleForAdminSetup");
			await SetRoleForAdmin();
			await _identityUnitOfWork.SaveChangesAsync();

			await _identityUnitOfWork.CommitTransactionAsync();
		}
		catch (Exception ex)
		{
			_logger.LogCritical(ex, "An error occurred while populating the Identity database.");
			throw;
		}
	}

	private async Task EnsurePendingMigrationsAppliedAsync()
	{
		try
		{
			var migrations = await _identityUnitOfWork.GetPendingMigrationsAsync();

			if (migrations.Any())
			{
				await _identityUnitOfWork.MigrateAsync();
			}
		}
		catch
		{
			await _identityUnitOfWork.RollbackTransactionAsync();
			throw;
		}
	}

	private async Task EnsureRolesPopulatedAsync()
	{
		IRoleRepository roleRepository = _identityUnitOfWork.RoleRepository;

		if (!await roleRepository.ContainsAnyAsync())
		{
			ICollection<RoleModel> defaultRoles = new List<RoleModel>();
			StringBuilder idBuilder = new();

			foreach (KeyValuePair<string, string> pair in BasicRoles)
			{
				idBuilder.Append(pair.Key.ToLower());
				idBuilder.Append(RoleIdSuffix);

				defaultRoles.Add(new RoleModel()
				{
					Id = idBuilder.ToString(),
					Name = pair.Key,
					Description = pair.Value
				});

				idBuilder.Clear();
			}

			await roleRepository.AddRangeAsync(defaultRoles);
		}
	}

	private async Task EnsureAdminPopulatedAsync()
	{
		IUserRepository userRepository = _identityUnitOfWork.UserRepository;

		if (!await userRepository.ContainsAnyAsync())
		{
			await userRepository.AddAsync(AdminInitModel);
		}
	}

	private async Task SetRoleForAdmin()
	{
		IUserRepository userRepository = _identityUnitOfWork.UserRepository;
		IRoleRepository roleRepository = _identityUnitOfWork.RoleRepository;

		UserModel? adminUser = await userRepository.GetWithDetailsAsync(AdminId);
		string roleId = string.Concat(AdminRoleName.ToLower(), RoleIdSuffix);
		RoleModel? roleForAdmin = await roleRepository
			.GetEntity(roleId)
			.SingleOrDefaultAsync();

		if (adminUser != null && !adminUser.UserRoles.Any() && roleForAdmin != null)
		{
			UserRoleModel roleModel = new(adminUser, roleForAdmin)
			{
				UserId = adminUser.Id,
				RoleId = roleForAdmin.Id,
			};

			adminUser.UserRoles.Add(roleModel);
		}
	}
}
