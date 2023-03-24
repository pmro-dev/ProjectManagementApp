using EntityFramework.Exceptions.SqlServer;
using Identity_Domain_Entities;
using Identity_Domain_Entities.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;
using TODO_List_ASPNET_MVC.Infrastructure.Helpers;
using TODO_List_ASPNET_MVC.Models.DataBases.AppDb;
using TODO_List_ASPNET_MVC.Models.DataBases.IdentityDb;
using TODO_List_ASPNET_MVC.Models.DataBases.Repositories;

namespace TODO_List_ASPNET_MVC
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

			builder.Services.AddDbContext<IAppDbContext, AppDbContext>(
				opts =>
				{
					opts.UseSqlServer(builder.Configuration[HelperProgram.ConnectionStringMainDb]);
					opts.UseExceptionProcessor();
				},
				ServiceLifetime.Scoped);

			builder.Services.AddDbContext<IdentityDbContext, IdentityDbContext>(
				opts =>
				{
					opts.UseSqlServer(builder.Configuration[HelperProgram.ConnectionStringIdentityDb]);
					opts.UseExceptionProcessor();
				},
				ServiceLifetime.Scoped);

			builder.Services.AddScoped<IContextOperations, ContextOperations>();
			builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();
			builder.Services.AddScoped<SeedData>();

			#endregion

			#region SETUP AUTHENTICATION

			string AuthGoogleClientId = builder.Configuration[HelperProgram.AuthGoogleClientId];
			string AuthGoogleClientSecret = builder.Configuration[HelperProgram.AuthGoogleClientSecret];

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultScheme = HelperProgram.DefaultScheme;
				options.DefaultChallengeScheme = HelperProgram.DefaultChallengeScheme;
			})
			#region COOKIE AUTHENTICATION
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
				{
					options.AccessDeniedPath = HelperProgram.AccessDeniedPath;
					options.LoginPath = HelperProgram.LoginPath;
					options.ExpireTimeSpan = TimeSpan.FromDays(30);
					options.Cookie.HttpOnly = true;
					options.Cookie.Name = HelperProgram.CustomCookieName;
					options.Events = new CookieAuthenticationEvents()
					{
						OnSigningIn = async context =>
						{
							ILogger? _logger = context.HttpContext.RequestServices.GetService<ILogger>();
							IdentityDbContext? identityDbContext = context.HttpContext.RequestServices.GetService<IdentityDbContext>();
							ClaimsIdentity? principle = context.Principal?.Identity as ClaimsIdentity;

							if (identityDbContext is null)
							{
								_logger?.LogCritical(Messages.ErrorDbContextIsNull, nameof(identityDbContext));
								throw new InvalidOperationException(Messages.DbContextIsNull(nameof(identityDbContext)));
							}

							SetAuthSchemeClaimForUser(context, out Claim authSchemeClaimWithProviderName, principle);
							UserModel userBasedOnProviderClaims = PrepareUserBasedOnProviderClaims(context, authSchemeClaimWithProviderName);

							if (await identityDbContext.DoesUserExist(userBasedOnProviderClaims.NameIdentifier))
							{
								await UpdateUserWhenDataOnProviderSideChangedAsync(identityDbContext, userBasedOnProviderClaims, authSchemeClaimWithProviderName);
							}
							else
							{
								await AddUserWhenNotExistInDbAsync(identityDbContext, userBasedOnProviderClaims);
							}

							await SetRolesForUserPrincipleAsync(identityDbContext, userBasedOnProviderClaims.UserId, principle);
						}
					};
				})
			#endregion
			#region OPEN_ID_CONNECT AUTHENTICATION
				.AddOpenIdConnect(HelperProgram.GoogleOpenIDScheme, options =>
				{
					options.Authority = HelperProgram.GoogleAuthority;
					options.ClientId = AuthGoogleClientId;
					options.ClientSecret = AuthGoogleClientSecret;
					options.CallbackPath = HelperProgram.GoogleOpenIdCallBackPath;
					options.SaveTokens = true;
					options.Scope.Add(HelperProgram.GoogleEmailScope);
				});
			#endregion

			#region PRIVATE SETUP AUTHENTICATION

			void SetAuthSchemeClaimForUser(CookieSigningInContext context, out Claim authSchemeClaimWithProviderName, ClaimsIdentity? principle)
			{
				var authScheme = context.Properties.Items.SingleOrDefault(i => i.Key == HelperProgram.AuthSchemeClaimKey);
				authSchemeClaimWithProviderName = new Claim(authScheme.Key, authScheme.Value ?? HelperProgram.AuthSchemeClaimValue);

				principle?.AddClaim(authSchemeClaimWithProviderName);
			}


			async Task UpdateUserWhenDataOnProviderSideChangedAsync(IdentityDbContext identityDbContext, UserModel userBasedOnProviderClaims, Claim authSchemeClaimWithProviderName)
			{
				UserModel userFromDb = await identityDbContext.GetUserAsync(userBasedOnProviderClaims.NameIdentifier);
				bool doesUserUseOtherProvider = userBasedOnProviderClaims.Provider != CookieAuthenticationDefaults.AuthenticationScheme;

				if (doesUserUseOtherProvider && ModelsHelper.IsUserDataFromProviderDifferentToUserDataInDb(ref userBasedOnProviderClaims, ref userFromDb))
				{
					userFromDb.FirstName = userBasedOnProviderClaims.FirstName;
					userFromDb.Lastname = userBasedOnProviderClaims.Lastname;
					userFromDb.Username = userBasedOnProviderClaims.Username;
					userFromDb.Provider = authSchemeClaimWithProviderName.Value;
					userFromDb.Email = userBasedOnProviderClaims.Email;

					await identityDbContext.UpdateUserAsync(userFromDb);
					await identityDbContext.SaveChangesAsync();
				}
			}


			async Task AddUserWhenNotExistInDbAsync(IdentityDbContext identityDbContext, UserModel userBasedOnProviderClaims)
			{
				await AddNewUserToDbAsync(identityDbContext, userBasedOnProviderClaims);
			}


			async Task SetRolesForUserPrincipleAsync(IdentityDbContext identityDbContext, string userId, ClaimsIdentity? principle)
			{
				var userRoleNames = await identityDbContext.GetRolesForUserAsync(userId);

				foreach (string roleName in userRoleNames)
				{
					principle?.AddClaim(new Claim(ClaimTypes.Role, roleName));
				}
			}


			UserModel PrepareUserBasedOnProviderClaims(CookieSigningInContext context, Claim authSchemeClaimWithProviderName)
			{
				List<Claim> claims = context.Principal?.Claims.ToList() ?? new();

				UserModel userBasedOnClaims = new()
				{
					NameIdentifier = claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value,
					UserId = claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value,
					Username = claims.Single(c => c.Type == ClaimTypes.Name).Value,
					Provider = authSchemeClaimWithProviderName.Value,
					Email = claims.Single(c => c.Type == ClaimTypes.Email).Value
				};

				if (claims.Exists(c => c.Type == ClaimTypes.Name))
				{
					userBasedOnClaims.Username = claims.Single(c => c.Type == ClaimTypes.Name).Value;
				}

				return userBasedOnClaims;
			}


			async Task AddNewUserToDbAsync(IdentityDbContext identityDbContext, UserModel userBasedOnClaims)
			{
				RoleModel defaultRole = await identityDbContext.Roles.SingleAsync(r => r.Name == HelperProgram.DefaultRole);
				userBasedOnClaims.UserRoles.Add(new UserRoleModel() { Role = defaultRole, User = userBasedOnClaims });
				identityDbContext.Users.Add(userBasedOnClaims);
				await identityDbContext.SaveChangesAsync();
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
				app.UseExceptionHandler(HelperProgram.ErrorHandlingPath);
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
				name: HelperProgram.DefaultRouteName,
				pattern: HelperProgram.DefaultRoutePattern);

			#endregion


			#region POPULATE DATABASES

			await IdentitySeedData.EnsurePopulated(app, app.Logger);
			await DbSeeder.EnsurePopulated(app, app.Logger);

			#endregion

			app.Run();
		}
	}
}