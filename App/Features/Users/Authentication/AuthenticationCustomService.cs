using App.Common;
using App.Features.Exceptions.Throw;
using App.Features.Users.Authentication.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.Features.Users.Authentication;

public class AuthenticationCustomService : IAuthenticationCustomService
{
	private readonly ILogger<AuthenticationCustomService> _logger;

	public AuthenticationCustomService(ILogger<AuthenticationCustomService> logger)
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
			_logger.LogError(ExceptionsMessages.LogUnableToAuthenticateUserPrincipal, nameof(AuthenticateUser), nameof(AuthenticationCustomService));
			return false;
		}

		return userPrincipal.Identities.Any(claimsIdentity => claimsIdentity.IsAuthenticated);
	}
}
