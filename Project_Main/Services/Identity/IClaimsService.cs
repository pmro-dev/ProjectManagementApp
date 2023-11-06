using Project_Main.Models.DTOs;
using System.Security.Claims;

namespace Project_Main.Services.Identity
{
    public interface IClaimsService
    {
        public ClaimsPrincipal CreateUserClaimsPrincipal(IUserDto userDto);
    }
}
