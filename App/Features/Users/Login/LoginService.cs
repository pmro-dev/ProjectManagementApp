using App.Features.Users.Authentication;
using App.Features.Users.Common.Claims;
using App.Features.Users.Common.Interfaces;
using App.Features.Users.Interfaces;
using App.Features.Users.Login.Interfaces;
using App.Infrastructure.Databases.Identity.Interfaces;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace App.Features.Users.Login;

public class LoginService : ILoginService
{
	//private readonly IIdentityUnitOfWork _identityUnitOfWork;
	private readonly IUserRepository _userRepository;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IClaimsService _claimsService;
	private readonly IUserAuthenticationService _userAuthenticationService;
	private readonly ILogger<LoginService> _logger;
	private readonly IUserMapper _accountMapper;

	public LoginService(
	IIdentityUnitOfWork identityUnitOfWork,
	IHttpContextAccessor httpContextAccessor,
	IClaimsService claimsService,
	IUserAuthenticationService userAuthenticationService,
		IUserMapper accountMapper,
		ILogger<LoginService> logger)
	{
		//_identityUnitOfWork = identityUnitOfWork;
		_userRepository = identityUnitOfWork.UserRepository;
		_httpContextAccessor = httpContextAccessor;
		_claimsService = claimsService;
		_userAuthenticationService = userAuthenticationService;
		_accountMapper = accountMapper;
		_logger = logger;
	}

	public async Task<bool> CheckIsUserAlreadyRegisteredAsync(ILoginInputDto loginInputDto)
	{
		IUserModel loggingUser = _accountMapper.TransferToUserModel(loginInputDto);
		return await _userRepository.ContainsAny(dbUser => dbUser.Username == loggingUser.Username && dbUser.Password == loggingUser.Password);
	}

	public async Task<bool> LogInUserAsync(ILoginInputDto loginInputDto)
	{
		IUserModel? loggingUserModel = _accountMapper.TransferToUserModel(loginInputDto);
		loggingUserModel = await _userRepository.GetByNameAndPasswordAsync(loggingUserModel.Username, loggingUserModel.Password);

		if (loggingUserModel is null) return false;

		IUserDto userDto = _accountMapper.TransferToUserDto(loggingUserModel);

		ClaimsPrincipal userClaimsPrincipal = _claimsService.CreateUserClaimsPrincipal(userDto);
		AuthenticationProperties authProperties = _userAuthenticationService.CreateDefaultAuthProperties();

		//TODO write logging
		HttpContext httpContext = _httpContextAccessor.HttpContext ?? throw new ArgumentException("Unable to get current HttpContext - accessor returned null HttpContext object.");

		await httpContext.SignInAsync(userClaimsPrincipal, authProperties);

		return true;
	}
}
