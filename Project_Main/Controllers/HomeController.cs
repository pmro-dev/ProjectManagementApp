using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.ViewModels.HomeViewModels;
using Project_Main.Controllers.Helpers;
using Project_Main.Services;
using Castle.Core.Internal;

namespace Project_Main.Controllers
{
	/// <summary>
	/// Controller to manage availability of page's resources via Authentication.
	/// </summary>
	[Authorize]
	[AllowAnonymous]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ILoginService _loginService;
		private readonly IRegisterUserService _registerUserService;
		private readonly IAuthenticationUserService _authenticationUserService;

		private string operationName = string.Empty;
		private readonly string controllerName = nameof(HomeController);

		public HomeController(ILoginService loginService, IRegisterUserService registerUserService, ILogger<HomeController> logger, IAuthenticationUserService authenticationUserService)
		{
			_loginService = loginService;
			_registerUserService = registerUserService;
			_logger = logger;
			_authenticationUserService = authenticationUserService;
		}

		/// <summary>
		/// Return Index of the whole page that is Login Page.
		/// </summary>
		/// <returns>Return user to Login Page.</returns>
		[HttpGet]
		public ViewResult Login() { return View(); }

		/// <summary>
		/// Method allows to login user with provided data by form.
		/// </summary>
		/// <param name="loginViewModel">Model with provided login data.</param>
		/// <returns>Redirect user to specific index view or to login view if authentication failed.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (ModelState.IsValid)
			{
				if (loginViewModel.Name.IsNullOrEmpty() || loginViewModel.Password.IsNullOrEmpty())
				{
					return View(loginViewModel);
				}

				string userName = loginViewModel.Name;
				string userPassword = loginViewModel.Password;

				try 
				{
					bool isUserRegistered = await _loginService.IsUserRegisteredAsync(userName, userPassword);

					if (isUserRegistered)
					{
						bool isLoggedInSuccessfully = await _loginService.LogInUserAsync();

						if (isLoggedInSuccessfully)
						{
							return RedirectToRoute(CustomRoutes.MainBoardRouteName);
						}
					}

					ModelState.AddModelError(string.Empty, Messages.InvalidLoginData);
				}
				catch (Exception ex)
				{
					// TODO change message and operationName aslast param
					_logger.LogCritical(ex, "SOME ERROR OCURRED WHILE LOGGING USER", nameof(Login));
					throw;
				}
			}

			return View(loginViewModel);
		}

		/// <summary>
		/// Setup returnUrl to main view of app and pass it to challenge url, also with provider name.
		/// </summary>
		/// <param name="provider">Authentication provider name.</param>
		/// <returns>Challenge for a certain Authentication.</returns>
		[HttpGet]
		[Route(CustomRoutes.LoginByProviderRoute)]
		public IActionResult LoginByProvider([FromRoute] string provider)
		{
			bool doesUserExistAndIsAuthenticated = User != null && User.Identities.Any(i => i.IsAuthenticated);

			if (doesUserExistAndIsAuthenticated)
			{
				return RedirectToRoute(CustomRoutes.MainBoardRouteName);
			}
			else
			{
				return _authenticationUserService.ChallengeProviderToLogin(provider);
			}
		}

		/// <summary>
		/// Method logout user from account.
		/// </summary>
		/// <returns>Return Login View.</returns>
		public async Task<IActionResult> LogOut()
		{
			string userAuthScheme = User.Claims.First(c => c.Type == ConfigConstants.AuthSchemeClaimKey).Value;

			switch (userAuthScheme)
			{
				case ConfigConstants.GoogleOpenIDScheme:
					await HttpContext.SignOutAsync();
					return Redirect(ConfigConstants.GoogleUrlToLogout);
				case CookieAuthenticationDefaults.AuthenticationScheme:
					await HttpContext.SignOutAsync();
					return RedirectToAction(nameof(Login));
				default:
					return new SignOutResult(new[] { CookieAuthenticationDefaults.AuthenticationScheme, userAuthScheme });
			}
		}

		/// <summary>
		/// Return Register view.
		/// </summary>
		/// <returns>Return user to Register Page.</returns>
		public ViewResult Register() { return View(); }

		/// <summary>
		/// Method allows to register new user identity.
		/// </summary>
		/// <param name="registerViewModel">Model with provided register data.</param>
		/// <returns>Redirect user to login page.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			if (ModelState.IsValid)
			{
				operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Register), controllerName);

				string? userName = registerViewModel.Name;
				string? userPassword = registerViewModel.Password;
				string? userEmail = registerViewModel.Email;

				bool isDataInvalid = userName.IsNullOrEmpty() || userPassword.IsNullOrEmpty() || userEmail.IsNullOrEmpty();

				if (isDataInvalid) return View();

				try
				{
					bool isPossibleToRegisterUser = await _registerUserService.IsPossibleToRegisterUserByProvidedData(userName);
					
					if (isPossibleToRegisterUser)
					{
						bool isUserRegisteredSuccessfully = await _registerUserService.RegisterUserAsync(userName, userPassword, userEmail);

						if (isUserRegisteredSuccessfully)
						{
							return View(nameof(Login));
						}
					}

					return View();
				}
				catch (Exception ex)
				{
					_logger.LogCritical(ex, Messages.LogCreatingUserIdentityFailed, operationName);
					throw;
				}
			}

			return View();
		}

		/// <summary>
		/// Return main error page for production's user.
		/// </summary>
		/// <returns>Return Error View.</returns>
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View();
		}
	}
}