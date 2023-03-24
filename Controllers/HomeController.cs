using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TODO_List_ASPNET_MVC.Models.ViewModels;
using TODO_List_ASPNET_MVC.Infrastructure.Helpers;

namespace TODO_List_ASPNET_MVC.Controllers
{
	/// <summary>
	/// Controller to manage availability of page's resources via Authentication.
	/// </summary>
	[Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
		private readonly ILogger<HomeController> _logger;
		private string operationName = string.Empty;
		private readonly string controllerName = nameof(HomeController);

		/// <summary>
		/// Initializes class.
		/// </summary>
		/// <param name="userManager">Manager to manage user feature.</param>
		/// <param name="signInManager">Manager to manage sign feature.</param>
		public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<HomeController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
			_logger = logger;
		}

        /// <summary>
        /// Return Index of the whole page that is Login Page.
        /// </summary>
        /// <returns>Return user to Login Page.</returns>
		[AllowAnonymous]
        public ViewResult Login()
        {
            return this.View();
        }
		
        /// <summary>
        /// Method allows to login user with provided data by form.
        /// </summary>
        /// <param name="loginViewModel">Model with provided login data.</param>
        /// <returns>Redirect user to specific index view or to login view if authentication failed.</returns>
		[HttpPost]
		[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginViewModel.Name);

                if (user != null)
                {
                    await signInManager.SignOutAsync();

					if ((await signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false)).Succeeded)
                    {
                        return RedirectToAction("Briefly", TodoListController.ShortName);
                    }
                }

                ModelState.AddModelError(string.Empty, Messages.InvalidLoginData);
            }

            return View(loginViewModel);
        }

		/// <summary>
		/// Method logout user from account.
		/// </summary>
		/// <returns>Return Login View.</returns>
		public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return View(nameof(Login));
        }

		/// <summary>
		/// Return Register view.
		/// </summary>
		/// <returns>Return user to Register Page.</returns>
		[AllowAnonymous]
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
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			if (this.ModelState.IsValid)
			{
				operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(Register), controllerName);

				try
				{
					IdentityUser user = await userManager.FindByNameAsync(registerViewModel.Name);

					if (user is null)
					{
						user = new IdentityUser(registerViewModel.Name)
						{
							Email = registerViewModel.Email
						};

						var result = await userManager.CreateAsync(user, registerViewModel.Password);

						if (result.Succeeded)
						{
							return View(nameof(Login));
						}

						foreach (var error in result.Errors)
						{
							ModelState.AddModelError("", error.Description);
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