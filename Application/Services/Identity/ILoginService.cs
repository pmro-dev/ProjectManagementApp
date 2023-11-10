using Application.DTOs.ViewModels.Inputs.Abstract;

namespace Application.Services.Identity;

public interface ILoginService
{
	Task<bool> CheckIsUserAlreadyRegisteredAsync(ILoginInputDto loginInputDto);
	Task<bool> LogInUserAsync(ILoginInputDto loginInputDto);
}