using App.Features.Users.Authentication.Interfaces;
using App.Features.Users.Login.Interfaces;
using App.Features.Users.Login.Models;
using App.Infrastructure.Helpers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static App.Common.ControllersConsts;

namespace App.Features.Users.Login;

public class LoginUserHandler : IRequestHandler<LoginUserQuery, bool>, IRequestHandler<LoginUserByProviderQuery, IActionResult>
{
	private readonly ILogger<LoginUserHandler> _logger;
	private readonly ILoginService _loginService;
	private readonly IUserAuthenticationService _userAuthenticationService;
	private readonly IMapper _mapper;

	public LoginUserHandler(ILogger<LoginUserHandler> logger, ILoginService loginService, 
		IUserAuthenticationService userAuthenticationService, IMapper mapper)
	{
		_logger = logger;
		_loginService = loginService;
		_userAuthenticationService = userAuthenticationService;
		_mapper = mapper;
	}

	public async Task<bool> Handle(LoginUserQuery request, CancellationToken cancellationToken)
	{
		LoginInputDto loginInputDto = _mapper.Map<LoginInputDto>(request.InputVM);
		bool isLoginDataInvalid = !LoginDtoValidator.Valid(loginInputDto);

		if (isLoginDataInvalid)
		{
			return false;
			//TODO 
			//ModelState.AddModelError(string.Empty, MessagesPacket.InvalidLoginData);
			//return View(loginInputVM);
		}

		//try
		//{

		bool isUserNotRegistered = !await _loginService.CheckIsUserAlreadyRegisteredAsync(loginInputDto);

		if (isUserNotRegistered)
		{
			return false;
			//TODO
			//ModelState.AddModelError(string.Empty, MessagesPacket.InvalidLoginData);
			//return View();
		}

		bool isLoggedInFailed = !await _loginService.LogInUserAsync(loginInputDto);

		if (isLoggedInFailed)
		{
			return false;
			//_logger.LogError(MessagesPacket.LoginFailedForRegisteredUser, nameof(Login), loginInputDto.Username);
			//ModelState.AddModelError(string.Empty, MessagesPacket.UnableToLogin);
			//return View();
		}

		return true;

		//catch (Exception ex)
		//{
		//	_logger.LogCritical(ex, MessagesPacket.LogExceptionOccurredOnLogging);
		//	throw;
		//}
	}

	public Task<IActionResult> Handle(LoginUserByProviderQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenArgumentIsNullOrEmptyThrowError(nameof(LoginUserByProviderQuery), request.Provider, nameof(request.Provider), _logger);

		return Task<IActionResult>.Factory.StartNew(() =>
		{
			var isUserAuthenticated = _userAuthenticationService.AuthenticateUser(request.User);

			if (isUserAuthenticated)
				return new RedirectToActionResult(BoardsCtrl.BrieflyAction, BoardsCtrl.Name, null);
			else
				return _userAuthenticationService.ChallengeProviderToLogin(request.Provider);
		}, 
		cancellationToken);
	}
}
