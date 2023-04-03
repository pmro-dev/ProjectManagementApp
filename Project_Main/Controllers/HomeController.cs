using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.ViewModels.HomeViewModels;
using Project_IdentityDomainEntities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Project_Main.Models.DataBases.Identity;
using Project_Main.Controllers.Helpers;

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
		private readonly IIdentityUnitOfWork _identityUnitOfWork;
		private string operationName = string.Empty;
		private readonly string controllerName = nameof(HomeController);

		/// <summary>
		/// Initializes class.
		/// </summary>
		public HomeController(IIdentityUnitOfWork identityUnitOfWork, ILogger<HomeController> logger)
		{
			_identityUnitOfWork = identityUnitOfWork;
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

				IUserRepository userRepository = _identityUnitOfWork.UserRepository;

				UserModel? user = await userRepository.GetByNameAndPasswordAsync(loginViewModel.Name, loginViewModel.Password);

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

					Dictionary<string, string?> itemsForAuthProperties = new()
					{
						{ HelperProgramAndAuth.AuthSchemeClaimKey, CookieAuthenticationDefaults.AuthenticationScheme }
					};

					AuthenticationProperties authProperties = new(itemsForAuthProperties);
					await HttpContext.SignInAsync(userPrincipal, authProperties);

					return RedirectToRoute(CustomRoutes.MainBoardRouteName);
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
		[Route(CustomRoutes.LoginByProviderRoute)]
		public IActionResult LoginByProvider([FromRoute] string provider)
		{
			if (User != null && User.Identities.Any(i => i.IsAuthenticated))
			{
				return RedirectToRoute(CustomRoutes.MainBoardRouteName);
			}

			AuthenticationProperties authProperties = new()
			{
				RedirectUri = CustomRoutes.MainBoardFullRoute
			};

			return new ChallengeResult(provider, authProperties);
		}

		/// <summary>
		/// Method logout user from account.
		/// </summary>
		/// <returns>Return Login View.</returns>
		public async Task<IActionResult> LogOut()
		{
			string userAuthScheme = User.Claims.First(c => c.Type == HelperProgramAndAuth.AuthSchemeClaimKey).Value;

			switch (userAuthScheme)
			{
				case HelperProgramAndAuth.GoogleOpenIDScheme:
					await HttpContext.SignOutAsync();
					return Redirect(HelperProgramAndAuth.GoogleUrlToLogout);
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
			if (ModelState.IsValid)
			{
				operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Register), controllerName);

				if (registerViewModel.Name == null || registerViewModel.Password == null)
				{
					throw new ArgumentNullException(nameof(registerViewModel));
				}

				IUserRepository userRepository = _identityUnitOfWork.UserRepository;

				try
				{
					if (await userRepository.IsNameTakenAsync(registerViewModel.Name) is false)
					{
						UserModel newUser = new()
						{
							Email = registerViewModel.Email,
							FirstName = registerViewModel.Name,
							Lastname = registerViewModel.Name,
							Password = registerViewModel.Password,
							Provider = HelperProgramAndAuth.CustomProvider,
							Username = registerViewModel.Name,
						};

						newUser.NameIdentifier = newUser.UserId;

						//Task cos = new(async () => await _identityRepository.AddAsync(newUser));
						Task addUserTask = new(async () => await userRepository.AddAsync(newUser));
						addUserTask.Start();

						if (addUserTask.IsCompletedSuccessfully)
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