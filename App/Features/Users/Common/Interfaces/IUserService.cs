using App.Features.Users.Interfaces;
using System.Security.Claims;

namespace App.Features.Users.Common.Interfaces;

public interface IUserService
{
	string GetSignedInUserId();

    Task UpdateUserAsync(IUserDto userBasedOnProviderClaimsDto, Claim authSchemeClaimWithProviderName);

    Task AddUserAsync(IUserDto userDto);

    Task SetRolesForUserPrincipleAsync(string userId, ClaimsIdentity principle);
}