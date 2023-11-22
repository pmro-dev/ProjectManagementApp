using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Features.Users.Login;

public class LoginUserByProviderQuery : IRequest<IActionResult>
{
	public string Provider {  get; }

	public LoginUserByProviderQuery(string provider)
	{
		Provider = provider;
	}
}
