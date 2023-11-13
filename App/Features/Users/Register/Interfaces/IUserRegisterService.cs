using App.Features.Users.Interfaces;

namespace App.Features.Users.Register.Interfaces;

public interface IUserRegisterService
{
	Task<bool> RegisterAsync(IUserDto userDto);
}