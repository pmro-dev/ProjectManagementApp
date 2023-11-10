using Application.DTOs.Entities;
using System.Security.Claims;

namespace Application.Services.Identity;

public interface IClaimsService
{
	public ClaimsPrincipal CreateUserClaimsPrincipal(IUserDto userDto);
}
