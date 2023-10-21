using Project_Main.Infrastructure.DTOs;
using System.Security.Claims;

namespace Project_Main.Services.Identity
{
    public interface ILoginService
    {
        Task<bool> CheckIsUserAlreadyRegisteredAsync(LoginInputDto loginInputDto);
        Task<bool> LogInUserAsync(LoginInputDto loginInputDto);
	}
}