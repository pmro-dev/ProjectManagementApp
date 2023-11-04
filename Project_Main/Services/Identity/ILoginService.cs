using Project_Main.Infrastructure.DTOs.Inputs;

namespace Project_Main.Services.Identity
{
    public interface ILoginService
    {
        Task<bool> CheckIsUserAlreadyRegisteredAsync(ILoginInputDto loginInputDto);
        Task<bool> LogInUserAsync(ILoginInputDto loginInputDto);
	}
}