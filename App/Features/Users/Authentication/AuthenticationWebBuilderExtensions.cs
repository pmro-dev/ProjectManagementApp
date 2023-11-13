using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using App.Infrastructure.Databases.Identity.Interfaces;
using App.Features.Users.Interfaces;
using App.Infrastructure.Databases.Identity.Seeds;
using App.Infrastructure;
using App.Features.Users.Common.Models;
using App.Common.Helpers;
using App.Features.Users.Common.Roles;
using App.Infrastructure.Databases.Common.Helpers;

namespace App.Features.Users.Authentication;

/// <summary>
/// Web Builder extensions that allows to setup authentication services. 
/// </summary>
public static class AuthenticationWebBuilderExtensions
{
	/// <summary>
	/// Setup basic authentication based on Cookie.
	/// </summary>
	/// <param name="builder">App builder.</param>
	/// <exception cref="InvalidOperationException">Occurs when Unit Of Work, Gotten User from Db or Roles is null.</exception>
	/// <exception cref="ArgumentException">Occurs when Principle object of CookieContext is null.</exception>
	public static void SetupBasicAuthenticationWithCookie(this WebApplicationBuilder builder)
	{
		builder.Services
		.AddAuthentication(options =>
		{
			options.DefaultScheme = AuthenticationConsts.DefaultScheme;
			options.DefaultChallengeScheme = AuthenticationConsts.DefaultChallengeScheme;
		})
		.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
		{
			options.AccessDeniedPath = CustomRoutes.AccessDeniedPath;
			options.LoginPath = CustomRoutes.LoginPath;
			options.ExpireTimeSpan = TimeSpan.FromDays(30);
			options.Cookie.HttpOnly = true;
			options.Cookie.Name = AuthenticationConsts.CustomCookieName;
			options.Events = new CookieAuthenticationEvents()
			{
				OnSigningIn = async cookieSigningInContext =>
				{
					ILogger? logger = cookieSigningInContext.HttpContext.RequestServices.GetService<ILogger>();

					SetupIdentityUnitOfWork(cookieSigningInContext, logger, out IIdentityUnitOfWork _identityUnitOfWork, out IUserRepository userRepository, out IRoleRepository roleRepository);

					var authScheme = cookieSigningInContext.Properties.Items.SingleOrDefault(i => i.Key == AuthenticationConsts.AuthSchemeClaimKey);
					Claim authSchemeClaimWithProviderName = new(authScheme.Key, authScheme.Value ?? AuthenticationConsts.AuthSchemeClaimValue);

					CreateUserBasedOnProviderData(cookieSigningInContext, authSchemeClaimWithProviderName, out ClaimsIdentity principle, out IUserModel userBasedOnProviderClaims);

					if (await userRepository.IsNameTakenAsync(userBasedOnProviderClaims.Username))
					{
						await UpdateUserWhenDataOnProviderSideChangedAsync(userRepository, userBasedOnProviderClaims, authSchemeClaimWithProviderName, logger);
					}
					else
					{
						await AddUserWhenNotExistsAsync(userRepository, userBasedOnProviderClaims, roleRepository);
					}

					await _identityUnitOfWork.SaveChangesAsync();
					await SetRolesForUserPrincipleAsync(userRepository, userBasedOnProviderClaims.UserId, principle);
				}
			};
		});

		#region LOCAL FUNCTIONS

		static void SetupIdentityUnitOfWork(CookieSigningInContext cookieSigningInContext, ILogger? logger, out IIdentityUnitOfWork _identityUnitOfWork, out IUserRepository userRepository, out IRoleRepository roleRepository)
		{
			IIdentityUnitOfWork LoggAndThrowExceptionOnNullUnitOfWork()
			{
				logger?.LogCritical(MessagesPacket.LogErrorDbContextIsNull, nameof(_identityUnitOfWork));
				throw new InvalidOperationException(MessagesPacket.DbContextIsNull(nameof(_identityUnitOfWork)));
			}

			_identityUnitOfWork = cookieSigningInContext.HttpContext.RequestServices.GetService<IIdentityUnitOfWork>() ?? LoggAndThrowExceptionOnNullUnitOfWork();

			userRepository = _identityUnitOfWork.UserRepository;
			roleRepository = _identityUnitOfWork.RoleRepository;
		}


		static void CreateUserBasedOnProviderData(CookieSigningInContext cookieSigningInContext, Claim authSchemeClaimWithProviderName, out ClaimsIdentity principle, out IUserModel userBasedOnProviderClaims)
		{
			principle = cookieSigningInContext.Principal?.Identity as ClaimsIdentity ??
				throw new ArgumentException(MessagesPacket.ParamObjectNull(nameof(CreateUserBasedOnProviderData), nameof(cookieSigningInContext.Principal)));

			principle.AddClaim(authSchemeClaimWithProviderName);

			List<Claim> claims = cookieSigningInContext.Principal?.Claims.ToList() ?? new();

			IUserModel userBasedOnClaims = new UserModel()
			{
				NameIdentifier = claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value,
				UserId = claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value,
				Username = claims.Single(c => c.Type == ClaimTypes.GivenName).Value,
				Provider = authSchemeClaimWithProviderName.Value,
				Email = claims.Single(c => c.Type == ClaimTypes.Email).Value
			};

			userBasedOnProviderClaims = userBasedOnClaims;
		}


		static async Task UpdateUserWhenDataOnProviderSideChangedAsync(IUserRepository userRepository, IUserModel userBasedOnProviderClaims, Claim authSchemeClaimWithProviderName, ILogger? logger)
		{
			IUserModel? userFromDb = await userRepository.GetAsync(userBasedOnProviderClaims.NameIdentifier);

			if (userFromDb is null)
			{
				logger?.LogCritical(MessagesPacket.LogEntityNotFoundInDbSet, nameof(UpdateUserWhenDataOnProviderSideChangedAsync), userBasedOnProviderClaims.NameIdentifier, HelperDatabase.UsersDbSetName);
				throw new InvalidOperationException(MessagesPacket.EntityNotFoundByIdInDb(nameof(UpdateUserWhenDataOnProviderSideChangedAsync), "Identity", -2));
			}

			bool doesUserUseOtherProvider = userBasedOnProviderClaims.Provider != CookieAuthenticationDefaults.AuthenticationScheme;

			if (doesUserUseOtherProvider && !userBasedOnProviderClaims.Equals(userFromDb))
			{
				userFromDb.FirstName = userBasedOnProviderClaims.FirstName;
				userFromDb.LastName = userBasedOnProviderClaims.LastName;
				userFromDb.Username = userBasedOnProviderClaims.Username;
				userFromDb.Provider = authSchemeClaimWithProviderName.Value;
				userFromDb.Email = userBasedOnProviderClaims.Email;

				userRepository.Update((UserModel)userFromDb);
			}
		}


		static async Task AddUserWhenNotExistsAsync(IUserRepository userRepository, IUserModel userBasedOnProviderClaims, IRoleRepository roleRepository)
		{
			IRoleModel? roleForNewUser = await roleRepository.GetByFilterAsync(r => r.Name == IdentitySeedData.DefaultRole);

			if (roleForNewUser is null)
			{
				//TODO add logging
				throw new InvalidOperationException("message");
			}

			userBasedOnProviderClaims.UserRoles.Add(new UserRoleModel()
			{
				User = userBasedOnProviderClaims,
				UserId = userBasedOnProviderClaims.UserId,
				Role = roleForNewUser,
				RoleId = roleForNewUser.Id
			});

			await userRepository.AddAsync((UserModel)userBasedOnProviderClaims);
		}


		static async Task SetRolesForUserPrincipleAsync(IUserRepository userRepository, string userId, ClaimsIdentity? principle)
		{
			IEnumerable<IRoleModel> userRoles = await userRepository.GetRolesAsync(userId);
			IEnumerable<string> userRolesNames = userRoles.Select(userRole => userRole.Name).ToList();

			foreach (string roleName in userRolesNames)
			{
				principle?.AddClaim(new Claim(ClaimTypes.Role, roleName));
			}
		}

		#endregion
	}

	/// <summary>
	/// Setup Google Authentication based on OpenIDConnect.
	/// </summary>
	/// <param name="builder">App builder.</param>
	public static void SetupGoogleAuthentication(this WebApplicationBuilder builder)
	{
		string AuthGoogleClientId = builder.Configuration[AuthenticationConsts.AuthGoogleClientId];
		string AuthGoogleClientSecret = builder.Configuration[AuthenticationConsts.AuthGoogleClientSecret];

		builder.Services.AddAuthentication(options =>
		{
			options.DefaultScheme = AuthenticationConsts.DefaultScheme;
			options.DefaultChallengeScheme = AuthenticationConsts.DefaultChallengeScheme;
		})
			.AddOpenIdConnect(AuthenticationConsts.GoogleOpenIDScheme, options =>
			{
				options.Authority = AuthenticationConsts.GoogleAuthority;
				options.ClientId = AuthGoogleClientId;
				options.ClientSecret = AuthGoogleClientSecret;
				options.CallbackPath = AuthenticationConsts.GoogleOpenIdCallBackPath;
				options.SaveTokens = true;
				options.Scope.Add(AuthenticationConsts.GoogleEmailScope);
			});
	}
}
