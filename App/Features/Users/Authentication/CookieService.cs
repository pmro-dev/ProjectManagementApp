using App.Features.Users.Authentication.Interfaces;
using App.Infrastructure;
using App.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace App.Features.Users.Authentication;

public class CookieService : ICookieService
{
	private readonly ILogger<CookieService> _logger;
	private readonly ICookieEventsService _cookieEventsService;

	public CookieService(ILogger<CookieService> logger, ICookieEventsService cookieEventsService)
	{
		_logger = logger;
		_cookieEventsService = cookieEventsService;
	}

	public void SetupOptions(CookieAuthenticationOptions options)
	{
		ExceptionsService.ThrowWhenAuthOptionsObjectIsNull(nameof(SetupOptions), options, nameof(CookieAuthenticationOptions), _logger);

		options.AccessDeniedPath = CustomRoutes.AccessDeniedPath;
		options.LoginPath = CustomRoutes.LoginPath;
		options.ExpireTimeSpan = TimeSpan.FromDays(30);
		options.Cookie.HttpOnly = true;
		options.Cookie.Name = AuthenticationConsts.CustomCookieName;

		options.Events = new CookieAuthenticationEvents()
		{
			OnSigningIn = async cookieSigningInContext => await _cookieEventsService.OnSigningInManageUserIdentityAsync(cookieSigningInContext)
		};
	}
}
