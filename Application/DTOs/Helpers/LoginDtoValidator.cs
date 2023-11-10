using Application.DTOs.ViewModels.Inputs.Abstract;
namespace Application.DTOs.Helpers;

public static class LoginDtoValidator
{
	public static bool Valid(ILoginInputDto loginInputDto)
	{
		return !(string.IsNullOrEmpty(loginInputDto.Username) || string.IsNullOrEmpty(loginInputDto.Password));
	}
}
