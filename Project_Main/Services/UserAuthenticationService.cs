using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Project_Main.Controllers.Helpers;

namespace Project_Main.Services
{
	public class UserAuthenticationService : IUserAuthenticationService
	{
		public UserAuthenticationService() { }

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
