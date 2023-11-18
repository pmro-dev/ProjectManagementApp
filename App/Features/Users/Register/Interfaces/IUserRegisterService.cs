using App.Features.Users.Common.Models;

namespace App.Features.Users.Register.Interfaces;

public interface IUserRegisterService
{
	Task<bool> RegisterAsync(UserDto userDto);
}