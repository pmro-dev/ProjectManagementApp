using Project_Main.Infrastructure.DTOs.Entities;
using System.Security.Claims;

namespace Project_Main.Services.Identity
{
    public interface IClaimsService
    {
        public ClaimsPrincipal CreateUserClaimsPrincipal(IUserDto userDto);
    }
}
