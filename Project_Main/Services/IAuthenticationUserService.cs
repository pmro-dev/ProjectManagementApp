using Microsoft.AspNetCore.Mvc;

namespace Project_Main.Services
{
	public interface IAuthenticationUserService
	{
		ChallengeResult ChallengeProviderToLogin(string provider);
	}
}