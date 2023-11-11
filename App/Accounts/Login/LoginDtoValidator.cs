using Web.Accounts.Login.Interfaces;

namespace Web.Accounts.Login;

public static class LoginDtoValidator
{
	public static bool Valid(ILoginInputDto loginInputDto)
	{
		return !(string.IsNullOrEmpty(loginInputDto.Username) || string.IsNullOrEmpty(loginInputDto.Password));
	}
}
