using App.Features.Exceptions.Throw;
using App.Features.Users.Authentication.Interfaces;
using App.Features.Users.Common.Interfaces;
using App.Features.Users.Common.Models;
using App.Infrastructure.Databases.Identity.Interfaces;
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

		UserDto signingUserDto = _identityService.CreateUser(identity, authenticationClaim);

		if (await _userRepository.IsAccountExistedAsync(signingUserDto.Email))
		{
			await _userService.UpdateUserModelAsync(signingUserDto, authenticationClaim);
		}
		else
		{
			await _userService.AddNewUserAsync(signingUserDto);
		}

		await _identityUnitOfWork.SaveChangesAsync();
		await _userService.SetRolesForUserPrincipleAsync(signingUserDto.UserId, identity);
	}


	#region LOCAL METHODS

	private Claim GetAuthenticationClaim(CookieSigningInContext cookieSigningInContext)
	{
		var authSchemeValuePair = cookieSigningInContext.Properties.Items.SingleOrDefault(authProperty => authProperty.Key == AuthenticationConsts.AuthSchemeClaimKey);
		ExceptionsService.WhenArgumentIsNullOrEmptyThrow(nameof(GetAuthenticationClaim), authSchemeValuePair.Value, nameof(authSchemeValuePair.Value), _logger);

		return new Claim(authSchemeValuePair.Key, authSchemeValuePair.Value!);
	}

	private ClaimsIdentity GetUserIdentity(CookieSigningInContext cookieSigningInContext)
	{
		ClaimsIdentity? identity = cookieSigningInContext.Principal?.Identity as ClaimsIdentity;
		ExceptionsService.WhenIdentityIsNullThrow(identity, _logger);

		return identity!;
	}

	#endregion
}
