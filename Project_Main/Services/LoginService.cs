using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Project_IdentityDomainEntities;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.Identity;
using System.Security.Claims;

namespace Project_Main.Services
{
	public class LoginService : ILoginService
	{
		private readonly IIdentityUnitOfWork _identityUnitOfWork;
		private readonly IUserRepository _userRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IClaimsService _claimsService;
		private readonly IUserAuthenticationService _userAuthenticationService;
		private UserModel? _user;

		public LoginService(IIdentityUnitOfWork identityUnitOfWork, IHttpContextAccessor httpContextAccessor, IClaimsService claimsService, IUserAuthenticationService userAuthenticationService)
		{
			_identityUnitOfWork = identityUnitOfWork;
			_userRepository = _identityUnitOfWork.UserRepository;
			_httpContextAccessor = httpContextAccessor;
			_claimsService = claimsService;
			_userAuthenticationService = userAuthenticationService;
		}

		public async Task<bool> CheckThatUserIsRegisteredAsync(string userName, string userPassword)
		{
			return await _userRepository.ContainsAny(dbUser => dbUser.Username == userName && dbUser.Password == userPassword);
			}

		public async Task<bool> LogInUserAsync(string userName, string userPassword)
		{
			_user = await _userRepository.GetByNameAndPasswordAsync(userName, userPassword);

			if (_user is not null)
			{
				ClaimsPrincipal userClaimsPrincipal = _claimsService.CreateUserClaimsPrincipal(_user);
				AuthenticationProperties authProperties = _userAuthenticationService.CreateDefaultAuthProperties();
				HttpContext httpContext = _httpContextAccessor.HttpContext ?? throw new ArgumentException("Unable to get current HttpContext - accessor returned null HttpContext object.");

				await httpContext.SignInAsync(userClaimsPrincipal, authProperties);

				return true;
			}

			throw new InvalidOperationException("You cannot to login user without checking his existence!");
		}
	}
}
