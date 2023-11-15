using App.Features.Users.Common.Roles;
using App.Features.Users.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace App.Features.Users.Common.Claims;

public class ClaimsService : IClaimsService
{
    public ClaimsPrincipal CreateUserClaimsPrincipal(IUserDto userDto)
	{
		var userClaims = CreateUserClaims(userDto);

		ClaimsIdentity userIdentity = new(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
		ClaimsPrincipal userPrincipal = new(userIdentity);

		return userPrincipal;
	}


	#region LOCAL METHODS

	private static IEnumerable<Claim> CreateUserClaims(IUserDto userDto)
	{
		var basicClaimsCount = 5;
		var userRolesCount = userDto.UserRoles.Count;
		var userClaimsCapacity = basicClaimsCount + userRolesCount;

		var userClaims = new List<Claim>(userClaimsCapacity)
				{
					new(ClaimTypes.Name, userDto.Username),
					new(ClaimTypes.Email, userDto.Email),
					new(ClaimTypes.NameIdentifier, userDto.NameIdentifier),
					new(ClaimTypes.Surname, userDto.LastName),
					new(ClaimTypes.GivenName, userDto.FirstName),
				};

		AddRolesToUserClaims(userClaims, userDto.UserRoles);

		return userClaims;
	}

	private static void AddRolesToUserClaims(ICollection<Claim> userClaims, IEnumerable<IUserRoleDto> userRolesDto)
	{
		foreach (var userRole in userRolesDto)
		{
			//TODO add error logging
			if (userRole.Role is null)
			{
				throw new ArgumentNullException(nameof(userRolesDto));
			}

			userClaims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
		}
	}

    #endregion
}
