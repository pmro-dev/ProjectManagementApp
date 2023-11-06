using Project_Main.Models.Inputs.DTOs;

namespace Project_Main.Models.DTOs.Helpers
{
    public static class LoginDtoValidator
    {
        public static bool Valid(ILoginInputDto loginInputDto)
        {
            return !(string.IsNullOrEmpty(loginInputDto.Username) || string.IsNullOrEmpty(loginInputDto.Password));
        }
    }
}
