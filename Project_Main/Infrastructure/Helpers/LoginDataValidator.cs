using Project_Main.Infrastructure.DTOs.Inputs;

namespace Project_Main.Infrastructure.Helpers
{
    public static class LoginDataValidator
	{
		public static bool Valid(ILoginInputDto loginInputDto)
		{
			return !(string.IsNullOrEmpty(loginInputDto.Username) || string.IsNullOrEmpty(loginInputDto.Password));
		}
	}
}
