using Microsoft.AspNetCore.Authentication.Cookies;
using Project_IdentityDomainEntities.Helpers;
using Project_IdentityDomainEntities;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.Identity;
using System.Security.Claims;
using Project_Main.Models.DataBases.Identity.DbSetup;
using Project_Main.Models.DataBases.Helpers;

namespace Project_Main
{
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
				options.DefaultScheme = ConfigConstants.DefaultScheme;
				options.DefaultChallengeScheme = ConfigConstants.DefaultChallengeScheme;
			})
			.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
			{
				options.AccessDeniedPath = CustomRoutes.AccessDeniedPath;
				options.LoginPath = CustomRoutes.LoginPath;
				options.ExpireTimeSpan = TimeSpan.FromDays(30);
				options.Cookie.HttpOnly = true;
				options.Cookie.Name = ConfigConstants.CustomCookieName;
				options.Events = new CookieAuthenticationEvents()
				{
					OnSigningIn = async cookieSigningInContext =>
					{
						ILogger? logger = cookieSigningInContext.HttpContext.RequestServices.GetService<ILogger>();

						SetupIdentityUnitOfWork(cookieSigningInContext, logger, out IIdentityUnitOfWork _identityUnitOfWork, out IUserRepository userRepository, out IRoleRepository roleRepository);

						var authScheme = cookieSigningInContext.Properties.Items.SingleOrDefault(i => i.Key == ConfigConstants.AuthSchemeClaimKey);
						Claim authSchemeClaimWithProviderName = new Claim(authScheme.Key, authScheme.Value ?? ConfigConstants.AuthSchemeClaimValue);

						CreateUserBasedOnProviderData(cookieSigningInContext, authSchemeClaimWithProviderName, out ClaimsIdentity principle, out UserModel userBasedOnProviderClaims);

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
					logger?.LogCritical(Messages.LogErrorDbContextIsNull, nameof(_identityUnitOfWork));
					throw new InvalidOperationException(Messages.DbContextIsNull(nameof(_identityUnitOfWork)));
				}

				_identityUnitOfWork = cookieSigningInContext.HttpContext.RequestServices.GetService<IIdentityUnitOfWork>() ?? LoggAndThrowExceptionOnNullUnitOfWork();

				userRepository = _identityUnitOfWork.UserRepository;
				roleRepository = _identityUnitOfWork.RoleRepository;
			}


			static void CreateUserBasedOnProviderData(CookieSigningInContext cookieSigningInContext, Claim authSchemeClaimWithProviderName, out ClaimsIdentity principle, out UserModel userBasedOnProviderClaims)
			{
				principle = cookieSigningInContext.Principal?.Identity as ClaimsIdentity ?? 
					throw new ArgumentException(Messages.ParamObjectNull(nameof(CreateUserBasedOnProviderData), nameof(cookieSigningInContext.Principal)));
				
				principle?.AddClaim(authSchemeClaimWithProviderName);

				List<Claim> claims = cookieSigningInContext.Principal?.Claims.ToList() ?? new();

				UserModel userBasedOnClaims = new()
				{
					NameIdentifier = claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value,
					UserId = claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value,
					Username = claims.Single(c => c.Type == ClaimTypes.Name).Value,
					Provider = authSchemeClaimWithProviderName.Value,
					Email = claims.Single(c => c.Type == ClaimTypes.Email).Value
				};

				userBasedOnProviderClaims = userBasedOnClaims;
			}


			static async Task UpdateUserWhenDataOnProviderSideChangedAsync(IUserRepository userRepository, UserModel userBasedOnProviderClaims, Claim authSchemeClaimWithProviderName, ILogger? logger)
			{
				UserModel? userFromDb = await userRepository.GetAsync(userBasedOnProviderClaims.NameIdentifier);

				if (userFromDb is null)
				{
					logger?.LogCritical(Messages.LogEntityNotFoundInDbSet, nameof(UpdateUserWhenDataOnProviderSideChangedAsync), userBasedOnProviderClaims.NameIdentifier, HelperDatabase.UsersDbSetName);
					throw new InvalidOperationException(Messages.EntityNotFoundInDbById(nameof(UpdateUserWhenDataOnProviderSideChangedAsync), "Identity", -2));
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


			static async Task AddUserWhenNotExistsAsync(IUserRepository userRepository, UserModel userBasedOnProviderClaims, IRoleRepository roleRepository)
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


			static async Task SetRolesForUserPrincipleAsync(IUserRepository userRepository, string userId, ClaimsIdentity? principle)
			{
				IEnumerable<RoleModel> userRoles = await userRepository.GetRolesAsync(userId);
				List<string> userRolesNames = userRoles.Select(userRole => userRole.Name).ToList();

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
			string AuthGoogleClientId = builder.Configuration[ConfigConstants.AuthGoogleClientId];
			string AuthGoogleClientSecret = builder.Configuration[ConfigConstants.AuthGoogleClientSecret];

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultScheme = ConfigConstants.DefaultScheme;
				options.DefaultChallengeScheme = ConfigConstants.DefaultChallengeScheme;
			})
				.AddOpenIdConnect(ConfigConstants.GoogleOpenIDScheme, options =>
				{
					options.Authority = ConfigConstants.GoogleAuthority;
					options.ClientId = AuthGoogleClientId;
					options.ClientSecret = AuthGoogleClientSecret;
					options.CallbackPath = ConfigConstants.GoogleOpenIdCallBackPath;
					options.SaveTokens = true;
					options.Scope.Add(ConfigConstants.GoogleEmailScope);
				});
		}
	}
}
