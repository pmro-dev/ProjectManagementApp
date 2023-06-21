using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Project_Main.Infrastructure.Helpers;

namespace Project_Main.Services
{
	public class LogoutService : ILogoutService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly string LoginControllerName = "Login";

		public LogoutService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<ActionResult> LogoutByProviderTypeAsync(ControllerBase controller, string userAuthenticationScheme)
		{
			HttpContext httpContext = _httpContextAccessor.HttpContext!;

			switch (userAuthenticationScheme)
			{
				case ConfigConstants.GoogleOpenIDScheme:
					await httpContext!.SignOutAsync();
					return controller.Redirect(ConfigConstants.GoogleUrlToLogout);
				
				case CookieAuthenticationDefaults.AuthenticationScheme:
					await httpContext.SignOutAsync();
					return controller.RedirectToAction(LoginControllerName);
				
				default:
					return new SignOutResult(new[] { CookieAuthenticationDefaults.AuthenticationScheme, userAuthenticationScheme });
			}
		}
	}
}
