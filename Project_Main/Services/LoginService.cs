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
		private readonly IHttpContextAccessor _httpContextAccessor;
		private UserModel? _user;

		public LoginService(IIdentityUnitOfWork identityUnitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			_identityUnitOfWork = identityUnitOfWork;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<bool> CheckThatUserIsRegisteredAsync(string userName, string userPassword)
		{
			IUserRepository userRepository = _identityUnitOfWork.UserRepository;

			_user = await userRepository.GetByNameAndPasswordAsync(userName, userPassword);

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
				List<Claim> userClaims = new()
					{
						new Claim(ClaimTypes.Name, _user.Username),
						new Claim(ClaimTypes.Email, _user.Email),
						new Claim(ClaimTypes.NameIdentifier, _user.NameIdentifier),
						new Claim(ClaimTypes.Surname, _user.Lastname),
						new Claim(ClaimTypes.GivenName, _user.FirstName),
					};

				foreach (var userRole in _user.UserRoles)
				{
					userClaims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
				}

				ClaimsIdentity userIdentity = new(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
				ClaimsPrincipal userPrincipal = new(userIdentity);

				Dictionary<string, string?> itemsForAuthProperties = new()
					{
						{
							ConfigConstants.AuthSchemeClaimKey,
							CookieAuthenticationDefaults.AuthenticationScheme
						}
					};

				AuthenticationProperties authProperties = new(itemsForAuthProperties);
				HttpContext httpContext = _httpContextAccessor.HttpContext ?? throw new ArgumentException("Unable to get current HttpContext - accessor returned null HttpContext object.");

				await httpContext.SignInAsync(userPrincipal, authProperties);

				return true;
			}

			throw new InvalidOperationException("You cannot to login user without checking his existence!");
		}
	}
}
