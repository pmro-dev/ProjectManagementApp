using App.Common.Helpers;
using App.Features.Users.Common.Interfaces;
using App.Features.Users.Interfaces;
using App.Infrastructure;
using App.Infrastructure.Databases.Identity.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace App.Features.Users.Authentication;

public class CookieService : ICookieService
{
	private readonly IIdentityUnitOfWork _identityUnitOfWork;
	private readonly IUserRepository _userRepository;
	private readonly IIdentityService _identityService;
	private readonly IUserService _userService;

	public CookieService(IIdentityUnitOfWork identityUnitOfWork, IIdentityService identityService, IUserService userService)
	{
		_identityUnitOfWork = identityUnitOfWork;
		_userRepository = _identityUnitOfWork.UserRepository;
		_identityService = identityService;
		_userService = userService;
	}

	public void SetupOptions(CookieAuthenticationOptions options)
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
				var authScheme = cookieSigningInContext.Properties.Items.SingleOrDefault(authProperty => authProperty.Key == AuthenticationConsts.AuthSchemeClaimKey);
				Claim authSchemeClaimWithProviderName = new(authScheme.Key, authScheme.Value ?? AuthenticationConsts.AuthSchemeClaimValue);
				
				ClaimsIdentity identity = cookieSigningInContext.Principal?.Identity as ClaimsIdentity ??
					throw new ArgumentException(MessagesPacket.ParamObjectNull("Creating user identity on signingin", nameof(cookieSigningInContext.Principal)));

				identity.AddClaim(authSchemeClaimWithProviderName);

				IUserDto user = _identityService.CreateUser(identity.Claims, authSchemeClaimWithProviderName);

				if (await _userRepository.IsNameTakenAsync(user.Username))
				{
					await _userService.UpdateUserAsync(user, authSchemeClaimWithProviderName);
				}
				else
				{
					await _userService.AddUserAsync(user);
				}

				await _identityUnitOfWork.SaveChangesAsync();
				await _userService.SetRolesForUserPrincipleAsync(user.UserId, identity);
			}
		};
	}
}
