using App.Common.Helpers;
using App.Features.Users.Authentication.Interfaces;
using App.Features.Users.Common.Claims;
using App.Features.Users.Common.Models;
using App.Features.Users.Login.Interfaces;
using App.Infrastructure.Databases.Identity.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace App.Features.Users.Login;

public class LoginService : ILoginService
{
	private readonly IUserRepository _userRepository;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IClaimsService _claimsService;
	private readonly IUserAuthenticationService _userAuthenticationService;
	private readonly ILogger<LoginService> _logger;
	private readonly IMapper _mapper;

	public LoginService(
	IIdentityUnitOfWork identityUnitOfWork,
	IHttpContextAccessor httpContextAccessor,
	IClaimsService claimsService,
	IUserAuthenticationService userAuthenticationService,
		ILogger<LoginService> logger,
		IMapper mapper)
	{
		_userRepository = identityUnitOfWork.UserRepository;
		_httpContextAccessor = httpContextAccessor;
		_claimsService = claimsService;
		_userAuthenticationService = userAuthenticationService;
		_logger = logger;
		_mapper = mapper;
	}

	public async Task<bool> CheckIsUserAlreadyRegisteredAsync(LoginInputDto loginInputDto)
	{
		return await _userRepository.ContainsAny(dbUser => dbUser.Username == loginInputDto.Username && dbUser.Password == loginInputDto.Password);
	}

	public async Task<bool> LogInUserAsync(LoginInputDto loginInputDto)
	{
		UserModel? loggingUserModel = await _userRepository.GetByNameAndPasswordAsync(loginInputDto.Username, loginInputDto.Password);

		if (loggingUserModel is null) return false;

		UserDto userDto = _mapper.Map<UserDto>(loggingUserModel);

		ClaimsPrincipal userPrincipal = _claimsService.CreateUserClaimsPrincipal(userDto);
		AuthenticationProperties authProperties = _userAuthenticationService.CreateDefaultAuthProperties();

		//TODO write logging
		HttpContext? httpContext = _httpContextAccessor.HttpContext;

		if (httpContext is null)
		{
			_logger.LogCritical(MessagesPacket.LogHttpContextObjectIsNull, nameof(LogInUserAsync));
			throw new ArgumentException("Unable to get current HttpContext - accessor returned null HttpContext object.");
		}

		await httpContext.SignInAsync(userPrincipal, authProperties);

		return true;
	}
}
