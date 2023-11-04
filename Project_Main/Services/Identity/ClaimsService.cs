using Microsoft.AspNetCore.Authentication.Cookies;
using Project_IdentityDomainEntities;
using Project_Main.Infrastructure.DTOs.Entities;
using System.Security.Claims;

namespace Project_Main.Services.Identity
{
    public class ClaimsService : IClaimsService
    {
        public ClaimsPrincipal CreateUserClaimsPrincipal(IUserDto userDto)
        {
            var userClaims = CreateUserClaims(userDto);

            ClaimsIdentity userClaimsIdentity = new(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal userClaimsPrincipal = new(userClaimsIdentity);

            return userClaimsPrincipal;
        }

		#region Local Methods

		private static List<Claim> CreateUserClaims(IUserDto userDto)
        {
            var userClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userDto.Username),
                        new Claim(ClaimTypes.Email, userDto.Email),
                        new Claim(ClaimTypes.NameIdentifier, userDto.NameIdentifier),
                        new Claim(ClaimTypes.Surname, userDto.LastName),
                        new Claim(ClaimTypes.GivenName, userDto.FirstName),
                    };

            AddRolesToUserClaims(userClaims, userDto.UserRoles);

            return userClaims;
        }

        private static void AddRolesToUserClaims(List<Claim> userClaims, List<UserRoleModel> newClaims)
        {
            foreach (var userRole in newClaims)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }
        }

		#endregion
	}
}
