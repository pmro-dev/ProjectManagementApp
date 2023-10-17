using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Project_Main.Services
{
	public interface IUserAuthenticationService
	{
		ChallengeResult ChallengeProviderToLogin(string provider);

		AuthenticationProperties CreateDefaultAuthProperties();

	}
}