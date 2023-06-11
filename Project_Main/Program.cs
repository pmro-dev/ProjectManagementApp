using EntityFramework.Exceptions.SqlServer;
using Project_IdentityDomainEntities;
using Project_IdentityDomainEntities.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.AppData.DbSetup;
using Project_Main.Models.DataBases.AppData;
using Project_Main.Models.DataBases.Identity.DbSetup;
using Project_Main.Models.DataBases.Identity;
using Project_Main.Controllers.Helpers;
using ClassLibrary_SeedData;

namespace Project_Main
{
    public static class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Configuration.AddEnvironmentVariables().AddUserSecrets(Assembly.GetExecutingAssembly(), true);

			#region SETUP LOGGERS

			builder.Logging.ClearProviders();
			builder.Logging.AddConsole();

			#endregion

			#region SETUP ENVIRONMENT SETTINGS

			if (builder.Environment.IsDevelopment())
			{
				builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
			}
			else
			{
				builder.Services.AddControllersWithViews();
			}

			#endregion

			#region SETUP DATA BASES SERVICES

			builder.Services.AddDbContext<CustomAppDbContext>(
			opts =>
				{
					opts.UseSqlServer(builder.Configuration[HelperProgramAndAuth.ConnectionStringMainDb]);
					opts.UseExceptionProcessor();
				},
				ServiceLifetime.Scoped);

			builder.Services.AddDbContext<CustomIdentityDbContext>(
			opts =>
				{
					opts.UseSqlServer(builder.Configuration[HelperProgramAndAuth.ConnectionStringIdentityDb]);
					opts.UseExceptionProcessor();
				},
				ServiceLifetime.Scoped);

			builder.Services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();
			builder.Services.AddScoped<IDataUnitOfWork, DataUnitOfWork>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IRoleRepository, RoleRepository>();
			builder.Services.AddScoped<ITodoListRepository, TodoListRepository>();
			builder.Services.AddScoped<ITaskRepository, TaskRepository>();

			builder.Services.AddScoped<SeedData>();

			#endregion

			#region SETUP AUTHENTICATION

			string AuthGoogleClientId = builder.Configuration[HelperProgramAndAuth.AuthGoogleClientId];
			string AuthGoogleClientSecret = builder.Configuration[HelperProgramAndAuth.AuthGoogleClientSecret];

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultScheme = HelperProgramAndAuth.DefaultScheme;
				options.DefaultChallengeScheme = HelperProgramAndAuth.DefaultChallengeScheme;
			})
			#region COOKIE AUTHENTICATION
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
				{
					options.AccessDeniedPath = CustomRoutes.AccessDeniedPath;
					options.LoginPath = CustomRoutes.LoginPath;
					options.ExpireTimeSpan = TimeSpan.FromDays(30);
					options.Cookie.HttpOnly = true;
					options.Cookie.Name = HelperProgramAndAuth.CustomCookieName;
					options.Events = new CookieAuthenticationEvents()
					{
						OnSigningIn = async cookieSigningInContext =>
						{
							ILogger? _logger = cookieSigningInContext.HttpContext.RequestServices.GetService<ILogger>();
							IIdentityUnitOfWork? _identityUnitOfWork = cookieSigningInContext.HttpContext.RequestServices.GetService<IIdentityUnitOfWork>();

							if (_identityUnitOfWork is null)
							{
								// TODO write new message for Unit Of Work null object
								_logger?.LogCritical(Messages.ErrorDbContextIsNull, nameof(_identityUnitOfWork));
								throw new InvalidOperationException(Messages.DbContextIsNull(nameof(_identityUnitOfWork)));
							}

							IUserRepository userRepository = _identityUnitOfWork.UserRepository;
							IRoleRepository roleRepository = _identityUnitOfWork.RoleRepository;

							ClaimsIdentity? principle = cookieSigningInContext.Principal?.Identity as ClaimsIdentity;
							SetAuthSchemeClaimForUser(cookieSigningInContext, out Claim authSchemeClaimWithProviderName, principle);
							UserModel userBasedOnProviderClaims = PrepareUserBasedOnProviderClaims(cookieSigningInContext, authSchemeClaimWithProviderName);

							if (await userRepository.IsNameTakenAsync(userBasedOnProviderClaims.Username))
							{
								await UpdateUserWhenDataOnProviderSideChangedAsync(userRepository, userBasedOnProviderClaims, authSchemeClaimWithProviderName);
							}
							else
							{
								await AddUserWhenItNotExistAsync(userRepository, userBasedOnProviderClaims, roleRepository);
							}

							await _identityUnitOfWork.SaveChangesAsync();
							await SetRolesForUserPrincipleAsync(userRepository, userBasedOnProviderClaims.UserId, principle);
						}
					};
				})
			#endregion
			#region OPEN_ID_CONNECT AUTHENTICATION
				.AddOpenIdConnect(HelperProgramAndAuth.GoogleOpenIDScheme, options =>
				{
					options.Authority = HelperProgramAndAuth.GoogleAuthority;
					options.ClientId = AuthGoogleClientId;
					options.ClientSecret = AuthGoogleClientSecret;
					options.CallbackPath = HelperProgramAndAuth.GoogleOpenIdCallBackPath;
					options.SaveTokens = true;
					options.Scope.Add(HelperProgramAndAuth.GoogleEmailScope);
				});
			#endregion

			#region PRIVATE SETUP AUTHENTICATION

			void SetAuthSchemeClaimForUser(CookieSigningInContext context, out Claim authSchemeClaimWithProviderName, ClaimsIdentity? principle)
			{
				var authScheme = context.Properties.Items.SingleOrDefault(i => i.Key == HelperProgramAndAuth.AuthSchemeClaimKey);
				authSchemeClaimWithProviderName = new Claim(authScheme.Key, authScheme.Value ?? HelperProgramAndAuth.AuthSchemeClaimValue);

				principle?.AddClaim(authSchemeClaimWithProviderName);
			}


			async Task UpdateUserWhenDataOnProviderSideChangedAsync(IUserRepository userRepository, UserModel userBasedOnProviderClaims, Claim authSchemeClaimWithProviderName)
			{
				UserModel? userFromDb = await userRepository.GetAsync(userBasedOnProviderClaims.NameIdentifier);

				if (userFromDb is null)
				{
					// TODO logger
					//_logger?.LogCritical(Messages.EntityNotFoundInDbLogger, );
					throw new InvalidOperationException(Messages.EntityNotFoundInDb(nameof(UpdateUserWhenDataOnProviderSideChangedAsync), "Identity", -2));
				}

				bool doesUserUseOtherProvider = userBasedOnProviderClaims.Provider != CookieAuthenticationDefaults.AuthenticationScheme;

				if (doesUserUseOtherProvider && ModelsHelper.IsUserDataFromProviderDifferentToUserDataInDb(ref userBasedOnProviderClaims, ref userFromDb))
				{
					userFromDb.FirstName = userBasedOnProviderClaims.FirstName;
					userFromDb.Lastname = userBasedOnProviderClaims.Lastname;
					userFromDb.Username = userBasedOnProviderClaims.Username;
					userFromDb.Provider = authSchemeClaimWithProviderName.Value;
					userFromDb.Email = userBasedOnProviderClaims.Email;

					await userRepository.Update(userFromDb);
				}
			}


			async Task AddUserWhenItNotExistAsync(IUserRepository userRepository, UserModel userBasedOnProviderClaims, IRoleRepository roleRepository)
			{
				RoleModel? roleForNewUser = await roleRepository.GetSingleByFilterAsync(r => r.Name == IdentitySeedData.DefaultRole);

				if (roleForNewUser is null)
				{
					throw new InvalidOperationException("message");
				}

				userBasedOnProviderClaims.UserRoles.Add(new UserRoleModel() 
				{
					User = userBasedOnProviderClaims,
					UserId = userBasedOnProviderClaims.UserId,
					Role = roleForNewUser,
					RoleId = roleForNewUser.Id
				});

				await userRepository.AddAsync(userBasedOnProviderClaims);
			}


			async Task SetRolesForUserPrincipleAsync(IUserRepository userRepository, string userId, ClaimsIdentity? principle)
			{
				IEnumerable<RoleModel> userRoles = await userRepository.GetRolesAsync(userId);
				List<string> userRolesNames = userRoles.Select(userRole => userRole.Name).ToList();

				foreach (string roleName in userRolesNames)
				{
					principle?.AddClaim(new Claim(ClaimTypes.Role, roleName));
				}
			}


			UserModel PrepareUserBasedOnProviderClaims(CookieSigningInContext cookieSigningInContext, Claim authSchemeClaimWithProviderName)
			{
				List<Claim> claims = cookieSigningInContext.Principal?.Claims.ToList() ?? new();

				UserModel userBasedOnClaims = new()
				{
					NameIdentifier = claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value,
					UserId = claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value,
					Username = claims.Single(c => c.Type == ClaimTypes.Name).Value,
					Provider = authSchemeClaimWithProviderName.Value,
					Email = claims.Single(c => c.Type == ClaimTypes.Email).Value
				};

				return userBasedOnClaims;
			}

			#endregion

			#endregion


			#region SETUP PIPELINE

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler(CustomRoutes.ErrorHandlingPath);
				app.UseHsts();
			}

			app.UseStaticFiles();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			#endregion


			#region SETUP ROUTES

			app.MapControllerRoute(
				name: CustomRoutes.DefaultRouteName,
				pattern: CustomRoutes.DefaultRoutePattern);

			#endregion


			#region POPULATE DATABASES

			await IdentitySeedData.EnsurePopulated(app, app.Logger);
			await DbSeeder.EnsurePopulated(app, app.Logger);

			#endregion

			app.Run();
		}
	}
}