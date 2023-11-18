using App.Features.Users.Common.Models;
using System.Security.Claims;

namespace App.Features.Users.Common.Interfaces;

public interface IUserService
{
	string GetSignedInUserId();

    Task UpdateUserInDbAsync(UserDto userBasedOnProviderDataDto, Claim authenticationSchemeClaim);

    Task AddNewUserToDbAsync(UserDto userDto);

    Task SetRolesForUserPrincipleAsync(string userId, ClaimsIdentity identity);
}