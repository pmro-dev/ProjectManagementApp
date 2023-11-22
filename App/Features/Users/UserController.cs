#region USINGS

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static App.Common.ControllersConsts;
using App.Infrastructure;
using App.Infrastructure.Helpers;
using App.Features.Users.Login.Models;
using App.Features.Users.Register.Models;
using MediatR;
using App.Features.Users.Login;
using App.Features.Users.Logout;
using App.Features.Users.Register;

#endregion

namespace App.Features.Users;

/// <summary>
/// Controller to manage availability of page's resources via Authentication.
/// </summary>
public class UserController : Controller
{
	private readonly ILogger<UserController> _logger;
	private readonly IMediator _mediator;

	public UserController(ILogger<UserController> logger, IMediator mediator)
	{
		_logger = logger;
		_mediator = mediator;
	}

	/// <summary>
	/// Return Index of the whole page that is Login Page.
	/// </summary>
	/// <returns>Return user to Login Page.</returns>
	[HttpGet]
	[AllowAnonymous]
	public ViewResult Login() 
	{ 
		return View(); 
	}

	/// <summary>
	/// Method allows to login user with provided data by form.
	/// </summary>
	/// <param name="loginInputVM">Model with provided login data.</param>
	/// <returns>Redirect user to specific index view or to login view if authentication failed.</returns>
	[HttpPost]
	[AllowAnonymous]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginInputVM loginInputVM)
	{
		var result = await _mediator.Send(new LoginUserQuery(loginInputVM));

		if (!result)
		{
			return View(loginInputVM);
		}

		return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);
	}

	/// <summary>
	/// Setup returnUrl to main view of app and pass it to challenge url, also with provider name.
	/// </summary>
	/// <param name="provider">Authentication provider name.</param>
	/// <returns>Challenge for a certain Authentication.</returns>
	[HttpGet]
	[AllowAnonymous]
	[Route(CustomRoutes.LoginByProviderRoute)]
	public async Task<IActionResult> LoginByProvider([FromRoute] string provider)
	{
		ExceptionsService.WhenArgumentIsNullOrEmptyThrowError(nameof(LoginByProvider), provider, nameof(provider), _logger);

		var result = await _mediator.Send(new LoginUserByProviderQuery(provider, User));

		return result;
	}

	/// <summary>
	/// Method logout user from account.
	/// </summary>
	/// <returns>Return Login View.</returns>
	[Authorize]
	public async Task<IActionResult> LogOut()
	{
		var result = await _mediator.Send(new LogoutUserQuery());

		return result;
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
	/// <param name="registerInputVM">Model with provided register data.</param>
	/// <returns>Redirect user to login page.</returns>
	[HttpPost]
	[AllowAnonymous]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Register(RegisterInputVM registerInputVM)
	{
		var result = await _mediator.Send(new RegisterUserCommand(registerInputVM));

		if (!result)
			return BadRequest();

		return View(UserCtrl.LoginAction);
	}

	/// <summary>
	/// Return main error page for production's user.
	/// </summary>
	/// <returns>Return Error View.</returns>
	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	[AllowAnonymous]
	public IActionResult Error() 
	{ 
		return View(); 
	}
}