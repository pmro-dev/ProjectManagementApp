using App.Common.Helpers;
using App.Features.Exceptions.Throw;
using App.Features.Users.Authentication.Interfaces;
using App.Features.Users.Common.Interfaces;
using App.Features.Users.Login.Interfaces;
using App.Features.Users.Login.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static App.Common.ControllersConsts;

namespace App.Features.Users.Login;

public class LoginUserHandler : IRequestHandler<LoginUserQuery, LoginUserQueryResponse>, IRequestHandler<LoginUserByProviderQuery, LoginUserByProviderQueryResponse>
{
	private readonly ILogger<LoginUserHandler> _logger;
	private readonly ILoginService _loginService;
	private readonly IAuthenticationCustomService _authenticationService;
	private readonly IUserService _userService;
	private readonly IMapper _mapper;

	public LoginUserHandler(ILogger<LoginUserHandler> logger, ILoginService loginService,
		IAuthenticationCustomService authenticationService, IMapper mapper, IUserService userService)
	{
		_logger = logger;
		_loginService = loginService;
		_authenticationService = authenticationService;
		_mapper = mapper;
		_userService = userService;
	}

	public async Task<LoginUserQueryResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
	{
		LoginInputDto loginInputDto = _mapper.Map<LoginInputDto>(request.InputVM);
		bool isLoginDataInvalid = !LoginDtoValidator.Valid(loginInputDto);

		if (isLoginDataInvalid)
			return new LoginUserQueryResponse(ErrorMessagesHelper.InvalidLoginData, StatusCodesExtension.InvalidLoginData);


		bool isUserNotRegistered = !await _loginService.CheckIsUserAlreadyRegisteredAsync(loginInputDto);

		if (isUserNotRegistered)
			return new LoginUserQueryResponse(ErrorMessagesHelper.UserNotRegistered, StatusCodesExtension.UserNotRegistered);


		bool isLoggedInFailed = !await _loginService.LogInUserAsync(loginInputDto);

		if (isLoggedInFailed)
			return new LoginUserQueryResponse(ErrorMessagesHelper.UnableToLogin, StatusCodesExtension.UnableToLogin);

		return new LoginUserQueryResponse();
	}

	public async Task<LoginUserByProviderQueryResponse> Handle(LoginUserByProviderQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenArgumentIsNullOrEmptyThrow(nameof(LoginUserByProviderQuery), request.Provider, nameof(request.Provider), _logger);

		return await Task<LoginUserByProviderQueryResponse>.Factory.StartNew(() =>
		{
			ClaimsPrincipal userPrincipal = _userService.GetSignedInUser();
			var isUserAuthenticated = _authenticationService.AuthenticateUser(userPrincipal);
			IActionResult responseData;

			if (isUserAuthenticated)
				responseData = new RedirectToActionResult(BoardsCtrl.BrieflyAction, BoardsCtrl.Name, null);
			else
				responseData = _authenticationService.ChallengeProviderToLogin(request.Provider);

			return new LoginUserByProviderQueryResponse(responseData);
		},
		cancellationToken);
	}
}
