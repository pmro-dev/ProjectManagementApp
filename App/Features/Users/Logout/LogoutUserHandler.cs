using App.Features.Exceptions.Throw;
using App.Features.Users.Logout.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Features.Users.Logout;

public class LogoutUserHandler : IRequestHandler<LogoutUserQuery, LogoutUserQueryResponse>
{
	private readonly ILogoutService _logoutService;
	private readonly ILogger<LogoutUserHandler> _logger;

	public LogoutUserHandler(ILogoutService logoutService, ILogger<LogoutUserHandler> logger)
	{
		_logoutService = logoutService;
		_logger = logger;
	}

	public async Task<LogoutUserQueryResponse> Handle(LogoutUserQuery request, CancellationToken cancellationToken)
	{
		var actionResult = await _logoutService.LogoutAsync();

		if (actionResult is null)
		{
			_logger.LogCritical(ExceptionsMessages.LogCriticalModelObjectIsNull, nameof(LogoutUserQuery), nameof(IActionResult));
			throw new InvalidOperationException(ExceptionsMessages.ProvidedObjectIsNull);
		}

		return new LogoutUserQueryResponse(actionResult);
	}
}
