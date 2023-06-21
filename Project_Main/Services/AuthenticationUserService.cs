using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Project_Main.Controllers.Helpers;

namespace Project_Main.Services
{
	public class AuthenticationUserService : IAuthenticationUserService
	{
		public AuthenticationUserService() { }

		public ChallengeResult ChallengeProviderToLogin(string provider)
		{
			AuthenticationProperties authProperties = new()
			{
				RedirectUri = CustomRoutes.MainBoardFullRoute
			};

			return new ChallengeResult(provider, authProperties);
		}
	}
}
