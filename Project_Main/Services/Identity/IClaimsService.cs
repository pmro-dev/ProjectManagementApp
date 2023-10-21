using Project_Main.Infrastructure.DTOs;
using System.Security.Claims;

namespace Project_Main.Services.Identity
{
    public interface IClaimsService
    {
        public ClaimsPrincipal CreateUserClaimsPrincipal(UserDto userDto);
    }
}
