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
		private UserModel? _user;

		public LoginService(IIdentityUnitOfWork identityUnitOfWork, IHttpContextAccessor httpContextAccessor, IClaimsService claimsService)
		{
			_identityUnitOfWork = identityUnitOfWork;
			_userRepository = _identityUnitOfWork.UserRepository;
			_httpContextAccessor = httpContextAccessor;
			_claimsService = claimsService;
		}

		public async Task<bool> CheckThatUserIsRegisteredAsync(string userName, string userPassword)
		{
			_user = await _userRepository.GetByNameAndPasswordAsync(userName, userPassword);

			if (_user is null)
			{
				return false;
			}

			return true;
		}

		public async Task<bool> LogInUserAsync()
		{
			if (_user is not null)
			{

				foreach (var userRole in _user.UserRoles)
				{
					userClaims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
				}

				ClaimsPrincipal userClaimsPrincipal = _claimsService.CreateUserClaimsPrincipal(_user);


				ClaimsPrincipal userClaimsPrincipal = _claimsService.CreateUserClaimsPrincipal(_user);

				AuthenticationProperties authProperties = new(itemsForAuthProperties);
				HttpContext httpContext = _httpContextAccessor.HttpContext ?? throw new ArgumentException("Unable to get current HttpContext - accessor returned null HttpContext object.");

				await httpContext.SignInAsync(userClaimsPrincipal, authProperties);

				return true;
			}

			throw new InvalidOperationException("You cannot to login user without checking his existence!");
		}
	}
}
