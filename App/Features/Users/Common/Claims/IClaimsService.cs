using App.Features.Users.Common.Models;
using System.Security.Claims;

namespace App.Features.Users.Common.Claims;

public interface IClaimsService
{
	public ClaimsPrincipal CreateUserClaimsPrincipal(UserDto userDto);
}
