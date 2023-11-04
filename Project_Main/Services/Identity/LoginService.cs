using Microsoft.AspNetCore.Authentication;
using Project_IdentityDomainEntities;
using Project_Main.Infrastructure.DTOs.Entities;
using Project_Main.Infrastructure.DTOs.Inputs;
using Project_Main.Models.DataBases.Identity;
using Project_Main.Services.DTO;
using System.Security.Claims;

namespace Project_Main.Services.Identity
{
	public class LoginService : ILoginService
	{
		private readonly IIdentityUnitOfWork _identityUnitOfWork;
		private readonly IUserRepository _userRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IClaimsService _claimsService;
		private readonly IUserAuthenticationService _userAuthenticationService;
		private readonly ILogger<LoginService> _logger;

		public LoginService(
			IIdentityUnitOfWork identityUnitOfWork, 
			IHttpContextAccessor httpContextAccessor, 
			IClaimsService claimsService, 
			IUserAuthenticationService userAuthenticationService,
			ILogger<LoginService> logger)
		{
			_identityUnitOfWork = identityUnitOfWork;
			_userRepository = _identityUnitOfWork.UserRepository;
			_httpContextAccessor = httpContextAccessor;
			_claimsService = claimsService;
			_userAuthenticationService = userAuthenticationService;
			_logger = logger;
		}

		public async Task<bool> CheckIsUserAlreadyRegisteredAsync(LoginInputDto loginInputDto)
		{
			UserModel loggingUser = AccountDtoService.TransferToUserModel(loginInputDto);
			return await _userRepository.ContainsAny(dbUser => dbUser.Username == loggingUser.Username && dbUser.Password == loggingUser.Password);
		}

		public async Task<bool> LogInUserAsync(LoginInputDto loginInputDto)
		{
			UserModel? loggingUserModel = AccountDtoService.TransferToUserModel(loginInputDto);
			loggingUserModel = await _userRepository.GetByNameAndPasswordAsync(loggingUserModel.Username, loggingUserModel.Password);

			if (loggingUserModel is null) return false;

			UserDto userDto = AccountDtoService.TransferToUserDto(loggingUserModel);

			ClaimsPrincipal userClaimsPrincipal = _claimsService.CreateUserClaimsPrincipal(userDto);
			AuthenticationProperties authProperties = _userAuthenticationService.CreateDefaultAuthProperties();
			
			//TODO write logging
			HttpContext httpContext = _httpContextAccessor.HttpContext ?? throw new ArgumentException("Unable to get current HttpContext - accessor returned null HttpContext object.");

			await httpContext.SignInAsync(userClaimsPrincipal, authProperties);

			return true;
		}
	}
}
