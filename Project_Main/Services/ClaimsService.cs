using Microsoft.AspNetCore.Authentication.Cookies;
using Project_IdentityDomainEntities;
using System.Security.Claims;

namespace Project_Main.Services
{
	public class ClaimsService : IClaimsService
	{
		public ClaimsPrincipal CreateUserClaimsPrincipal(UserModel user)
		{
			var userClaims = CreateUserClaims(user);

			ClaimsIdentity userClaimsIdentity = new(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
			ClaimsPrincipal userClaimsPrincipal = new(userClaimsIdentity);

			return userClaimsPrincipal;
		}

		private static List<Claim> CreateUserClaims(UserModel user)
		{
			var userClaims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, user.Username),
						new Claim(ClaimTypes.Email, user.Email),
						new Claim(ClaimTypes.NameIdentifier, user.NameIdentifier),
						new Claim(ClaimTypes.Surname, user.LastName),
						new Claim(ClaimTypes.GivenName, user.FirstName),
					};

			AddRolesToUserClaims(userClaims, user.UserRoles);

			return userClaims;
		}

		private static void AddRolesToUserClaims(List<Claim> userClaims, List<UserRoleModel> newClaims)
		{
			foreach (var userRole in newClaims)
			{
				userClaims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
			}
		}
	}
}
