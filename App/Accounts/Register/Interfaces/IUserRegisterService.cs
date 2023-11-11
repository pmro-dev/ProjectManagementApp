using Web.Accounts.Users.Interfaces;

namespace Web.Accounts.Register.Interfaces;

public interface IUserRegisterService
{
    Task<bool> RegisterAsync(IUserDto userDto);
}