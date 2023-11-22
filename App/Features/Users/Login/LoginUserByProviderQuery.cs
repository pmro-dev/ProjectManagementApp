using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.Features.Users.Login;

public class LoginUserByProviderQuery : IRequest<IActionResult>
{
	public string Provider {  get; }
	public ClaimsPrincipal User { get; }

	public LoginUserByProviderQuery(string provider, ClaimsPrincipal user)
	{
		Provider = provider;
		User = user;
	}
}
