using Project_IdentityDomainEntities;
using MethodTimer;
using System.Text;
using Project_Main.Infrastructure.Helpers;

namespace Project_Main.Models.DataBases.Identity.DbSetup
{
    /// <summary>
    /// Class that manage seeding Identity data to database.
    /// </summary>
    public static class IdentitySeedData
    {
        public const string DefaultRole = "Guest";
        private static readonly string RoleIdSuffix = "RoleId";
        private const string AdminUser = "Admin";
        private const string AdminRoleName = "Admin";
        private const string AdminId = "adminId";
        private const string AdminPassword = "Secret123$";
        private const string AdminEmail = "admin@gmail.com";
        private const string ProviderName = HelperProgramAndAuth.DefaultScheme;

        private static readonly UserModel AdminInitModel = new()
        {
            UserId = AdminId,
            FirstName = AdminUser,
            Lastname = AdminUser,
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
                await SetRoleForAdmin(identityUnitOfWork);
                await identityUnitOfWork.SaveChangesAsync();
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

        [Time]
        private static async Task EnsureRolesPopulatedAsync(IIdentityUnitOfWork identityUnitOfWork)
        {
            IRoleRepository roleRepository = identityUnitOfWork.RoleRepository;

            if (!await roleRepository.ContainsAny())
            {
                List<RoleModel> defaultRoles = new();
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

        private static async Task EnsureAdminPopulatedAsync(IIdentityUnitOfWork identityUnitOfWork)
        {
            IUserRepository userRepository = identityUnitOfWork.UserRepository;

            if (!await userRepository.ContainsAny())
            {
                await userRepository.AddAsync(AdminInitModel);
            }
        }

        private static async Task SetRoleForAdmin(IIdentityUnitOfWork identityUnitOfWork)
        {
            IUserRepository userRepository = identityUnitOfWork.UserRepository;
            IRoleRepository roleRepository = identityUnitOfWork.RoleRepository;

            UserModel? adminUser = await userRepository.GetWithDetailsAsync(AdminId);
            string roleId = string.Concat(AdminRoleName.ToLower(), RoleIdSuffix);
            RoleModel? roleForAdmin = await roleRepository.GetAsync(roleId);

            if (adminUser != null && !adminUser.UserRoles.Any() && roleForAdmin != null)
            {
                UserRoleModel roleModel = new()
                {
                    UserId = adminUser.UserId,
                    User = adminUser,
                    RoleId = roleForAdmin.Id,
                    Role = roleForAdmin
                };

                adminUser.UserRoles.Add(roleModel);
            }
        }
    }
}
