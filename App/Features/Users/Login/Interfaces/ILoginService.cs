using App.Features.Users.Login.Models;

namespace App.Features.Users.Login.Interfaces;

public interface ILoginService
{
	Task<bool> CheckIsUserAlreadyRegisteredAsync(LoginInputDto loginInputDto);
	Task<bool> LogInUserAsync(LoginInputDto loginInputDto);
}