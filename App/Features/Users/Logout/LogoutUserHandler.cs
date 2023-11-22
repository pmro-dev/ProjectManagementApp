using App.Features.Users.Logout.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Features.Users.Logout;

public class LogoutUserHandler : IRequestHandler<LogoutUserQuery, IActionResult>
{
	private readonly ILogoutService _logoutService;

	public LogoutUserHandler(ILogoutService logoutService)
	{
		_logoutService = logoutService;
	}

	public async Task<IActionResult> Handle(LogoutUserQuery request, CancellationToken cancellationToken)
	{
		var result = await _logoutService.LogoutAsync();

		return result;
	}
}
