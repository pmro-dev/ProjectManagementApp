using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Services;
using Castle.Core.Internal;
using Project_Main.Models.ViewModels.InputModels;

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
		private readonly IUserRegisterService _userRegisterService;
		private readonly IUserAuthenticationService _userAuthenticationService;
		private readonly ILogoutService _logoutService;

		private string operationName = string.Empty;
		private readonly string controllerName = nameof(HomeController);

		public HomeController(ILoginService loginService, IUserRegisterService userRegisterService, ILogger<HomeController> logger, 
			IUserAuthenticationService userAuthenticationService, ILogoutService logoutService)
		{
			_loginService = loginService;
			_userRegisterService = userRegisterService;
			_logger = logger;
			_userAuthenticationService = userAuthenticationService;
			_logoutService = logoutService;
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
		/// <param name="loginInputVM">Model with provided login data.</param>
		/// <returns>Redirect user to specific index view or to login view if authentication failed.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginInputVM loginInputVM)
		{
			if (ModelState.IsValid)
			{
				string userName = loginInputVM.Name;
				string userPassword = loginInputVM.Password;

				if (userName.IsNullOrEmpty() || userPassword.IsNullOrEmpty()) 
					return View(loginInputVM);

				try 
				{
					bool isUserRegistered = await _loginService.CheckThatUserIsRegisteredAsync(userName, userPassword);

					if (isUserRegistered)
					{
						bool isLoggedInSuccessfully = await _loginService.LogInUserAsync();

						if (isLoggedInSuccessfully)
							return RedirectToRoute(CustomRoutes.MainBoardRouteName);
					}

					ModelState.AddModelError(string.Empty, Messages.InvalidLoginData);
				}
				catch (Exception ex)
				{
					_logger.LogCritical(ex, Messages.LogExceptionOccurredOnLogging);
					throw;
				}
			}

			return View(loginInputVM);
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
			if (provider.IsNullOrEmpty())
			{
				_logger.LogError(Messages.LogInvalidProviderName);
				throw new ArgumentNullException(nameof(provider));
			}

			bool doesUserExistAndIsAuthenticated = User != null && User.Identities.Any(i => i.IsAuthenticated);

			if (doesUserExistAndIsAuthenticated) 
				return RedirectToRoute(CustomRoutes.MainBoardRouteName);
			else 
				return _userAuthenticationService.ChallengeProviderToLogin(provider);
		}

		/// <summary>
		/// Method logout user from account.
		/// </summary>
		/// <returns>Return Login View.</returns>
		public async Task<IActionResult> LogOut()
		{
			string userAuthenticationScheme = User.Claims.First(c => c.Type == ConfigConstants.AuthSchemeClaimKey).Value;

			return await _logoutService.LogoutByProviderTypeAsync(this, userAuthenticationScheme);
		}

		/// <summary>
		/// Return Register view.
		/// </summary>
		/// <returns>Return user to Register Page.</returns>
		public ViewResult Register() { return View(); }

		/// <summary>
		/// Method allows to register new user identity.
		/// </summary>
		/// <param name="registerInputVM">Model with provided register data.</param>
		/// <returns>Redirect user to login page.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterInputVM registerInputVM)
		{
			if (ModelState.IsValid)
			{
				operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Register), controllerName);

				string? userName = registerInputVM.Name;
				string? userPassword = registerInputVM.Password;
				string? userEmail = registerInputVM.Email;

				bool isDataInvalid = userName.IsNullOrEmpty() || userPassword.IsNullOrEmpty() || userEmail.IsNullOrEmpty();

				if (isDataInvalid) 
					return View();

				try
				{
					bool isPossibleToRegisterUser = await _userRegisterService.IsPossibleToRegisterUserByProvidedData(userName);
					
					if (isPossibleToRegisterUser)
					{
						bool isUserRegisteredSuccessfully = await _userRegisterService.RegisterUserAsync(userName, userPassword, userEmail);

						if (isUserRegisteredSuccessfully)
							return View(nameof(Login));
					}

					return View();
				}
				catch (Exception ex)
				{
					_logger.LogCritical(ex, Messages.LogCreatingUserIdentityFailed, operationName);
					return Error();
				}
			}

			return View();
		}

		/// <summary>
		/// Return main error page for production's user.
		/// </summary>
		/// <returns>Return Error View.</returns>
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() { return View(); }
	}
}