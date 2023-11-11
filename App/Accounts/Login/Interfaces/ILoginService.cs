namespace Web.Accounts.Login.Interfaces;

public interface ILoginService
{
    Task<bool> CheckIsUserAlreadyRegisteredAsync(ILoginInputDto loginInputDto);
    Task<bool> LogInUserAsync(ILoginInputDto loginInputDto);
}