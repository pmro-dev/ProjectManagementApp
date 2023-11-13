﻿using App.Common.Helpers;
using App.Features.Users.Authentication;
using App.Features.Users.Logout.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using static App.Common.ControllersConsts;

namespace App.Features.Users.Logout;

public class LogoutService : ILogoutService
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly ILogger<LogoutService> _logger;

	public LogoutService(IHttpContextAccessor httpContextAccessor, ILogger<LogoutService> logger)
	{
		_httpContextAccessor = httpContextAccessor;
		_logger = logger;
	}

	public async Task<IActionResult> LogoutByProviderAsync()
	{
		HttpContext? httpContext = _httpContextAccessor.HttpContext;

		if (httpContext is null)
		{
			_logger.LogCritical(MessagesPacket.LogHttpContextNullOnLogout, nameof(LogoutByProviderAsync));
			throw new InvalidOperationException(MessagesPacket.HttpContextNullOnLogout);
		}

		string userAuthenticationScheme = httpContext.User.Claims.First(c => c.Type == AuthenticationConsts.AuthSchemeClaimKey).Value;

		IActionResult actionResult = userAuthenticationScheme switch
		{
			AuthenticationConsts.GoogleOpenIDScheme => new RedirectResult(AuthenticationConsts.GoogleUrlToLogout),
			CookieAuthenticationDefaults.AuthenticationScheme => new RedirectToActionResult(UserCtrl.LoginAction, UserCtrl.Name, null),
			_ => new SignOutResult(new[] { CookieAuthenticationDefaults.AuthenticationScheme, userAuthenticationScheme }),
		};

		await httpContext.SignOutAsync();
		return actionResult;
	}
}
