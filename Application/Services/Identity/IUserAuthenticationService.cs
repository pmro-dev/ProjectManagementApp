using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Application.Services.Identity;

public interface IUserAuthenticationService
{
	ChallengeResult ChallengeProviderToLogin(string provider);

	AuthenticationProperties CreateDefaultAuthProperties();

	public bool AuthenticateUser(ClaimsPrincipal userPrincipal);
}