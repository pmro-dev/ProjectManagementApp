using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Project_Main.Infrastructure.Helpers;

namespace Project_Main.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
	{
		public UserAuthenticationService() { }

		public ChallengeResult ChallengeProviderToLogin(string provider)
		{
			AuthenticationProperties authProperties = new()
			{
				RedirectUri = CustomRoutes.MainBoardRoute
			};

			return new ChallengeResult(provider, authProperties);
		}

		public AuthenticationProperties CreateDefaultAuthProperties()
		{
			Dictionary<string, string?> itemsForAuthProperties = new()
					{
						{
							ConfigConstants.AuthSchemeClaimKey,
							CookieAuthenticationDefaults.AuthenticationScheme
						}
					};

			AuthenticationProperties authProperties = new(itemsForAuthProperties);

			return authProperties;
		}
	}
}
