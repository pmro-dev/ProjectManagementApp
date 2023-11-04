using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Project_Main.Infrastructure.Helpers;

namespace Project_Main.Services.Identity
{
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

			string userAuthenticationScheme = httpContext.User.Claims.First(c => c.Type == ConfigConstants.AuthSchemeClaimKey).Value;

			IActionResult actionResult = userAuthenticationScheme switch
			{
				ConfigConstants.GoogleOpenIDScheme => new RedirectResult(ConfigConstants.GoogleUrlToLogout),
				CookieAuthenticationDefaults.AuthenticationScheme => new RedirectToActionResult(AccountCtrl.LoginAction, AccountCtrl.Name, null),
				_ => new SignOutResult(new[] { CookieAuthenticationDefaults.AuthenticationScheme, userAuthenticationScheme }),
			};

			await httpContext.SignOutAsync();
			return actionResult;
		}
	}
}
