﻿using Microsoft.AspNetCore.Authentication.Cookies;

namespace App.Features.Users.Authentication;

public static class AuthenticationConsts
{
	public const string AuthSchemeClaimValue = "nullClaimValue";

	public const string GoogleAuthority = "https://accounts.google.com";
	public const string AuthGoogleClientId = "AuthGoogle:ClientId";
	public const string AuthGoogleClientSecret = "AuthGoogle:ClientSecret";
	public const string GoogleEmailScope = "https://www.googleapis.com/auth/userinfo.email";
	public const string GoogleUrlToLogout = "https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:7103";
	public const string GoogleOpenIdCallBackPath = "/auth";
	public const string AuthSchemeClaimKey = ".AuthScheme";
	public const string DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	public const string DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	public const string GoogleOpenIDScheme = "google";
	public const string CustomCookieName = "ToDoCustomCookie";
}
