using Application.DTOs.Entities;
using Domain.Interfaces.ForIdentity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Application.Services.Identity;

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
					new Claim(ClaimTypes.Name, userDto.Username),
					new Claim(ClaimTypes.Email, userDto.Email),
					new Claim(ClaimTypes.NameIdentifier, userDto.NameIdentifier),
					new Claim(ClaimTypes.Surname, userDto.LastName),
					new Claim(ClaimTypes.GivenName, userDto.FirstName),
				};

		AddRolesToUserClaims(userClaims, userDto.UserRoles);

		return userClaims;
	}

	private static void AddRolesToUserClaims(ICollection<Claim> userClaims, IEnumerable<IUserRoleModel> userRoles)
	{
		foreach (var userRole in userRoles)
		{
			userClaims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
		}
	}

	#endregion
}
