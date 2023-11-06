using Project_Main.Models.Inputs.DTOs;

namespace Project_Main.Services.Identity
{
    public interface ILoginService
    {
        Task<bool> CheckIsUserAlreadyRegisteredAsync(ILoginInputDto loginInputDto);
        Task<bool> LogInUserAsync(ILoginInputDto loginInputDto);
	}
}