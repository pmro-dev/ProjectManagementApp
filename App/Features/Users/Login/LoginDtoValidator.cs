using App.Features.Users.Login.Interfaces;

namespace App.Features.Users.Login;

public static class LoginDtoValidator
{
	public static bool Valid(ILoginInputDto loginInputDto)
	{
		return !(string.IsNullOrEmpty(loginInputDto.Username) || string.IsNullOrEmpty(loginInputDto.Password));
	}
}
