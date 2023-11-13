using App.Features.Users.Interfaces;
using System.Security.Claims;

namespace App.Features.Users.Common.Claims;

public interface IClaimsService
{
	public ClaimsPrincipal CreateUserClaimsPrincipal(IUserDto userDto);
}
