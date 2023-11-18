using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static App.Common.ControllersConsts;
using App.Features.Users.Register;
using App.Features.Users.Common.Models;
using App.Features.Users.Login;
using App.Features.Users.Common;
using App.Features.Users.Login.Interfaces;
using App.Features.Users.Logout.Interfaces;
using App.Infrastructure;
using App.Infrastructure.Helpers;
using App.Common.Helpers;
using App.Features.Users.Register.Interfaces;
using AutoMapper;
using App.Features.Users.Authentication.Interfaces;

namespace App.Features.Users;

/// <summary>
/// Controller to manage availability of page's resources via Authentication.
/// </summary>
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly ILoginService _loginService;
    private readonly IUserRegisterService _userRegisterService;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly ILogoutService _logoutService;
    private readonly IMapper _mapper;

    private string operationName = string.Empty;
    private readonly string controllerName = nameof(UserController);

    public UserController(ILoginService loginService, IUserRegisterService userRegisterService, ILogger<UserController> logger,
        IUserAuthenticationService userAuthenticationService, ILogoutService logoutService, IMapper mapper)
    {
        _loginService = loginService;
        _userRegisterService = userRegisterService;
        _logger = logger;
        _userAuthenticationService = userAuthenticationService;
        _logoutService = logoutService;
        _mapper = mapper;
    }

    /// <summary>
    /// Return Index of the whole page that is Login Page.
    /// </summary>
    /// <returns>Return user to Login Page.</returns>
    [HttpGet]
	[AllowAnonymous]
	public ViewResult Login() { return View(); }

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
        if (ModelState.IsValid)
        {
            LoginInputDto loginInputDto = _mapper.Map<LoginInputDto>(loginInputVM);
            bool isLoginDataInvalid = !LoginDtoValidator.Valid(loginInputDto);

            if (isLoginDataInvalid)
            {
                ModelState.AddModelError(string.Empty, MessagesPacket.InvalidLoginData);
                return View(loginInputVM);
            }

            try
            {
                bool isUserNotRegistered = !await _loginService.CheckIsUserAlreadyRegisteredAsync(loginInputDto);

                if (isUserNotRegistered)
                {
                    ModelState.AddModelError(string.Empty, MessagesPacket.InvalidLoginData);
                    return View();
                }

                bool isLoggedInSuccessfully = await _loginService.LogInUserAsync(loginInputDto);

                if (isLoggedInSuccessfully) { return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name); }
                else
                {
                    _logger.LogError(MessagesPacket.LoginFailedForRegisteredUser, nameof(Login), loginInputDto.Username);
                    ModelState.AddModelError(string.Empty, MessagesPacket.UnableToLogin);
                    return View();
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, MessagesPacket.LogExceptionOccurredOnLogging);
                throw;
            }
        }

        return View();
    }

    /// <summary>
    /// Setup returnUrl to main view of app and pass it to challenge url, also with provider name.
    /// </summary>
    /// <param name="provider">Authentication provider name.</param>
    /// <returns>Challenge for a certain Authentication.</returns>
    [HttpGet]
	[AllowAnonymous]
	[Route(CustomRoutes.LoginByProviderRoute)]
    public IActionResult LoginByProvider([FromRoute] string provider)
    {
        ExceptionsService.ThrowExceptionWhenArgumentIsNullOrEmpty(nameof(LoginByProvider), provider, nameof(provider), _logger);

        var isUserAuthenticated = _userAuthenticationService.AuthenticateUser(User);

        if (isUserAuthenticated)
            return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);
        else
            return _userAuthenticationService.ChallengeProviderToLogin(provider);
    }

	/// <summary>
	/// Method logout user from account.
	/// </summary>
	/// <returns>Return Login View.</returns>
	[Authorize]
	public async Task<IActionResult> LogOut()
    {
        var actionResult = await _logoutService.LogoutAsync();

        return actionResult;
    }

    /// <summary>
    /// Return Register view.
    /// </summary>
    /// <returns>Return user to Register Page.</returns>
	[AllowAnonymous]
	public ViewResult Register() { return View(); }

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
        if (ModelState.IsValid)
        {
            UserDto userDto = _mapper.Map<UserDto>(registerInputVM);
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Register), controllerName);

            bool isDataInvalid = !UserDtoValidator.ValidData(userDto);

            if (isDataInvalid)
            {
                ModelState.AddModelError(string.Empty, MessagesPacket.InvalidRegisterData);
                return View();
            }

            try
            {
                bool isUserRegisteredSuccessfully = await _userRegisterService.RegisterAsync(userDto);

                if (isUserRegisteredSuccessfully)
                    return View(UserCtrl.LoginAction);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, MessagesPacket.LogCreatingUserIdentityFailed, operationName);
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
	[AllowAnonymous]
	public IActionResult Error() { return View(); }
}