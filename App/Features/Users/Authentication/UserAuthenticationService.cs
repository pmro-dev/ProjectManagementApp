using App.Common.Helpers;
using App.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.Features.Users.Authentication;

public class UserAuthenticationService : IUserAuthenticationService
{
	private readonly ILogger<UserAuthenticationService> _logger;

	public UserAuthenticationService(ILogger<UserAuthenticationService> logger)
	{
		_logger = logger;
	}

	public ChallengeResult ChallengeProviderToLogin(string provider)
	{
		AuthenticationProperties authProperties = new()
		{
			RedirectUri = CustomRoutes.MainBoardUri
		};

		return new ChallengeResult(provider, authProperties);
	}

	public AuthenticationProperties CreateDefaultAuthProperties()
	{
		Dictionary<string, string?> itemsForAuthProperties = new()
				{
					{
						AuthenticationConsts.AuthSchemeClaimKey,
						CookieAuthenticationDefaults.AuthenticationScheme
					}
				};

		AuthenticationProperties authProperties = new(itemsForAuthProperties);

		return authProperties;
	}

	public bool AuthenticateUser(ClaimsPrincipal userPrincipal)
	{
		if (userPrincipal is null)
		{
			_logger.LogError(MessagesPacket.UnableToAuthenticateUserPrincipal, nameof(AuthenticateUser), nameof(UserAuthenticationService));
			return false;
		}

		return userPrincipal.Identities.Any(claimsIdentity => claimsIdentity.IsAuthenticated);
	}
}
