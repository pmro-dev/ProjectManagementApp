using App.Features.Users.Authentication.Interfaces;
using App.Features.Users.Common.Interfaces;
using App.Features.Users.Common.Models;
using App.Infrastructure.Databases.Identity.Interfaces;
using App.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace App.Features.Users.Authentication;

public class CookieEventsService : ICookieEventsService
{
	private readonly IIdentityUnitOfWork _identityUnitOfWork;
	private readonly IUserRepository _userRepository;
	private readonly IIdentityService _identityService;
	private readonly IUserService _userService;
	private readonly ILogger<CookieEventsService> _logger;

	public CookieEventsService(IIdentityUnitOfWork identityUnitOfWork, IIdentityService identityService, IUserService userService, ILogger<CookieEventsService> logger)
	{
		_identityUnitOfWork = identityUnitOfWork;
		_userRepository = _identityUnitOfWork.UserRepository;
		_identityService = identityService;
		_userService = userService;
		_logger = logger;
	}

	public async Task OnSigningInManageUserIdentityAsync(CookieSigningInContext cookieSigningInContext)
	{
		Claim authenticationClaim = GetAuthenticationClaim(cookieSigningInContext);
		ClaimsIdentity identity = GetUserIdentity(cookieSigningInContext);

		UserDto signingUser = _identityService.CreateUser(identity, authenticationClaim);

		if (await _userRepository.IsAccountExistedAsync(signingUser.Email))
		{
			await _userService.UpdateUserInDbAsync(signingUser, authenticationClaim);
		}
		else
		{
			await _userService.AddNewUserAsync(signingUser);
		}

		await _identityUnitOfWork.SaveChangesAsync();
		await _userService.SetRolesForUserPrincipleAsync(signingUser.UserId, identity);
	}


	#region LOCAL METHODS

	private Claim GetAuthenticationClaim(CookieSigningInContext cookieSigningInContext)
	{
		var authSchemeValuePair = cookieSigningInContext.Properties.Items.SingleOrDefault(authProperty => authProperty.Key == AuthenticationConsts.AuthSchemeClaimKey);
		ExceptionsService.WhenArgumentIsNullOrEmptyThrowError(nameof(GetAuthenticationClaim), authSchemeValuePair.Value, nameof(authSchemeValuePair.Value), _logger);

		return new Claim(authSchemeValuePair.Key, authSchemeValuePair.Value!);
	}

	private ClaimsIdentity GetUserIdentity(CookieSigningInContext cookieSigningInContext)
	{
		ClaimsIdentity? identity = cookieSigningInContext.Principal?.Identity as ClaimsIdentity;
		ExceptionsService.WhenIdentityIsNullThrowCritical(identity, _logger);

		return identity!;
	}

	#endregion
}
