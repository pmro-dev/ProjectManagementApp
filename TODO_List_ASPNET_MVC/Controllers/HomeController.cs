using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using TODO_List_ASPNET_MVC.Infrastructure.Helpers;
using TODO_List_ASPNET_MVC.Models.ViewModels.HomeViewModels;
using Identity_Domain_Entities;
using TODO_List_ASPNET_MVC.Models.DataBases.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TODO_List_ASPNET_MVC.Controllers
{
	/// <summary>
	/// Controller to manage availability of page's resources via Authentication.
	/// </summary>
	[Authorize]
	[AllowAnonymous]
	public class HomeController : Controller
	{
		private const string GoogleUrlToLogout = "https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:7103";
		private readonly ILogger<HomeController> _logger;
		private string operationName = string.Empty;
		private readonly string controllerName = nameof(HomeController);
		private readonly IIdentityRepository _identityRepository;
		private const string returnToMain = "TodoList/All/Briefly";

		/// <summary>
		/// Initializes class.
		/// </summary>
		public HomeController(IIdentityRepository identityRepository, ILogger<HomeController> logger)
		{
			_identityRepository = identityRepository;
			_logger = logger;
		}

		/// <summary>
		/// Return Index of the whole page that is Login Page.
		/// </summary>
		/// <returns>Return user to Login Page.</returns>
		[HttpGet]
		public ViewResult Login()
		{
			return View();
		}

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
				if (loginViewModel.Name == null || loginViewModel.Password == null)
				{
					throw new ArgumentNullException(nameof(loginViewModel));
				}

				UserModel user = await _identityRepository.GetUserForLoggingAsync(loginViewModel.Name, loginViewModel.Password);
				//await context userManager.FindByNameAsync(loginViewModel.Name);

				if (user != null)
				{
					List<Claim> userClaims = new()
					{
						new Claim("username", user.Username),
						new Claim(ClaimTypes.Email, user.Email),
						new Claim(ClaimTypes.NameIdentifier, user.NameIdentifier),
						new Claim(ClaimTypes.Surname, user.Lastname),
						new Claim(ClaimTypes.Name, user.FirstName),
					};

					foreach (var userRole in user.UserRoles)
					{
						userClaims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name.ToString()));
					}

					ClaimsIdentity userIdentity = new(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
					ClaimsPrincipal userPrincipal = new(userIdentity);

					Dictionary<string, string?> items = new();
					items.Add(".AuthScheme", CookieAuthenticationDefaults.AuthenticationScheme);
					AuthenticationProperties authProperties = new(items);

					await HttpContext.SignInAsync(userPrincipal, authProperties);
					//await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

					return RedirectToAction("Briefly", TodoListController.ShortName);
				}

				ModelState.AddModelError(string.Empty, Messages.InvalidLoginData);
			}

			return View(loginViewModel);
		}

		/// <summary>
		/// Setup returnUrl to main view of app and pass it to challenge url, also with provider name.
		/// </summary>
		/// <param name="provider">Authentication provider name.</param>
		/// <returns>Challenge for a certain Authentication.</returns>
		[HttpGet]
		[Route("Login/{provider}")]
		public IActionResult LoginByProvider([FromRoute] string provider)
		{
			if (User != null && User.Identities.Any(i => i.IsAuthenticated))
			{
				RedirectToAction("Briefly", TodoListController.ShortName);
			}

			string returnUrl = returnToMain;
			AuthenticationProperties authProperties = new()
			{
				RedirectUri = returnUrl
			};

			return new ChallengeResult(provider, authProperties);
		}

		/// <summary>
		/// Method logout user from account.
		/// </summary>
		/// <returns>Return Login View.</returns>
		public async Task<IActionResult> LogOut()
		{
			string scheme = User.Claims.First(c => c.Type == ".AuthScheme").Value;

			switch (scheme)
			{
				case "google":
					await HttpContext.SignOutAsync();
					return Redirect(GoogleUrlToLogout);
				case CookieAuthenticationDefaults.AuthenticationScheme:
					await HttpContext.SignOutAsync();
					return RedirectToAction(nameof(Login));
				default:
					return new SignOutResult(new[] { CookieAuthenticationDefaults.AuthenticationScheme, scheme });
			}
		}

		/// <summary>
		/// Return Register view.
		/// </summary>
		/// <returns>Return user to Register Page.</returns>
		public ViewResult Register()
		{
			return View();
		}

		/// <summary>
		/// Method allows to register new user identity.
		/// </summary>
		/// <param name="registerViewModel">Model with provided register data.</param>
		/// <returns>Redirect user to login page.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			if (this.ModelState.IsValid)
			{
				operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(Register), controllerName);

				if (registerViewModel.Name == null || registerViewModel.Password == null)
				{
					throw new ArgumentNullException(nameof(registerViewModel));
				}

				try
				{
					if (await _identityRepository.IsUserNameUsedAsync(registerViewModel.Name) is false)
					{
						UserModel newUser = new()
						{
							Email = registerViewModel.Email,
							FirstName = registerViewModel.Name,
							Lastname = registerViewModel.Name,
							Password = registerViewModel.Password,
							Provider = "CustomProvider",
							Username = registerViewModel.Name,
						};

						newUser.NameIdentifier = newUser.UserId;

						bool result = await _identityRepository.AddUserAsync(newUser);

						if (result)
						{
							return View(nameof(Login));
						}
					}
				}
				catch (Exception ex)
				{
					_logger.LogCritical(ex, Messages.CreatingUserIdentityFailed, operationName);
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