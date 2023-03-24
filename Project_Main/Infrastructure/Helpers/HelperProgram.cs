using Microsoft.AspNetCore.Authentication.Cookies;

namespace Project_Main.Infrastructure.Helpers
{
	public static class HelperProgram
	{
		public const string DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		public const string DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		public const string GoogleOpenIDScheme = "google";
		public const string CustomCookieName = "ToDoCustomCookie";
		public const string GoogleAuthority = "https://accounts.google.com";
		public const string DefaultRole = "Guest";
		public const string AuthSchemeClaimKey = ".AuthScheme";
		public const string AuthSchemeClaimValue = "nullClaimValue";
		public const string ConnectionStringMainDb = "ConnectionStrings:TodoFinalAppDbConnection";
		public const string ConnectionStringIdentityDb = "ConnectionStrings:IdentityConnection";
		public const string AuthGoogleClientId = "AuthGoogle:ClientId";
		public const string AuthGoogleClientSecret = "AuthGoogle:ClientSecret";
		public const string GoogleOpenIdCallBackPath = "/auth";
		public const string ErrorHandlingPath = "/Home/Error";
		public const string AccessDeniedPath = "/Home/AccessDenied";
		public const string LoginPath = "/Home/Login";
		public const string DefaultRouteName = "default";
		public const string DefaultRoutePattern = "{controller=Home}/{action=Login}/{id?}";
		public const string GoogleEmailScope = "https://www.googleapis.com/auth/userinfo.email";
	}
}
