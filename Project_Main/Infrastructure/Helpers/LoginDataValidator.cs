using Project_Main.Infrastructure.DTOs;

namespace Project_Main.Infrastructure.Helpers
{
	public static class LoginDataValidator
	{
		public static bool Valid(LoginInputDto loginInputDto)
		{
			return string.IsNullOrEmpty(loginInputDto.Username) || string.IsNullOrEmpty(loginInputDto.Password);
		}
	}
}
