using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Features.Users.Login;

public class LoginUserByProviderQuery : IRequest<LoginUserByProviderQueryResponse>
{
	public string Provider {  get; }

	public LoginUserByProviderQuery(string provider)
	{
		Provider = provider;
	}
}

public record LoginUserByProviderQueryResponse(
	IActionResult? Data = null, 
	string? ErrorMessage = null, 
	int StatusCode = StatusCodes.Status200OK
){}
