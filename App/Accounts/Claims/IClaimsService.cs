using System.Security.Claims;
using Web.Accounts.Users.Interfaces;

namespace Web.Accounts.Claims;

public interface IClaimsService
{
    public ClaimsPrincipal CreateUserClaimsPrincipal(IUserDto userDto);
}
