using App.Common.Views;
using App.Features.Exceptions.Throw;
using App.Features.Users.Authentication.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace App.Features.Users.Authentication;

/// <summary>
/// Web Builder extensions that allows to setup authentication services. 
/// </summary>
public static class AuthenticationWebBuilderExtensions
{
	/// <summary>
	/// Setup basic authentication based on Cookie.
	/// </summary>
	/// <param name="builder">App builder.</param>
	/// <exception cref="InvalidOperationException">Occurs when Unit Of Work, Gotten User from Db or Roles is null.</exception>
	/// <exception cref="ArgumentException">Occurs when Principle object of CookieContext is null.</exception>
	public static void SetupBasicAuthenticationWithCookie(this WebApplicationBuilder builder)
	{
		//TODO try to get service in a different way if possible
		var cookieService = builder.Services
			.BuildServiceProvider()
			.CreateScope().ServiceProvider
			.GetRequiredService<ICookieService>();

		var logger = new LoggerFactory().CreateLogger<WebApplicationBuilder>();


		//TODO add error loggin
		if (cookieService is null)
		{
			logger.LogCritical(ExceptionsMessages.LackOfRequiredService(nameof(SetupBasicAuthenticationWithCookie), nameof(ICookieService)), nameof(SetupBasicAuthenticationWithCookie), nameof(ICookieService));
			throw new InvalidOperationException();
		}

		builder.Services
		.AddAuthentication(options =>
		{
			options.DefaultScheme = AuthenticationConsts.DefaultScheme;
			options.DefaultChallengeScheme = AuthenticationConsts.DefaultChallengeScheme;
		})
		.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => cookieService.SetupOptions(options));
	}

	/// <summary>
	/// Setup Google Authentication based on OpenIDConnect.
	/// </summary>
	/// <param name="builder">App builder.</param>
	public static void SetupGoogleAuthentication(this WebApplicationBuilder builder)
	{
		string AuthGoogleClientId = builder.Configuration[AuthenticationConsts.AuthGoogleClientId];
		string AuthGoogleClientSecret = builder.Configuration[AuthenticationConsts.AuthGoogleClientSecret];

		builder.Services.AddAuthentication(options =>
		{
			options.DefaultScheme = AuthenticationConsts.DefaultScheme;
			options.DefaultChallengeScheme = AuthenticationConsts.DefaultChallengeScheme;
		})
			.AddOpenIdConnect(AuthenticationConsts.GoogleOpenIDScheme, options =>
			{
				options.Authority = AuthenticationConsts.GoogleAuthority;
				options.ClientId = AuthGoogleClientId;
				options.ClientSecret = AuthGoogleClientSecret;
				options.CallbackPath = AuthenticationConsts.GoogleOpenIdCallBackPath;
				options.SaveTokens = true;
				options.Scope.Add(AuthenticationConsts.GoogleEmailScope);
			});
	}
}
