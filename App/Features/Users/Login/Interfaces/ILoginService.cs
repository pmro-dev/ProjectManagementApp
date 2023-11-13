namespace App.Features.Users.Login.Interfaces;

public interface ILoginService
{
	Task<bool> CheckIsUserAlreadyRegisteredAsync(ILoginInputDto loginInputDto);
	Task<bool> LogInUserAsync(ILoginInputDto loginInputDto);
}