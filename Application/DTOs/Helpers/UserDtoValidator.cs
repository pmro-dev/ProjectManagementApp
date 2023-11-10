using Application.DTOs.Entities;

namespace Application.DTOs.Helpers;

public static class UserDtoValidator
{
    public static bool ValidData(IUserDto userDto)
    {
        return !(string.IsNullOrEmpty(userDto.Username) ||
            string.IsNullOrEmpty(userDto.FirstName) ||
            string.IsNullOrEmpty(userDto.LastName) ||
            string.IsNullOrEmpty(userDto.Password) ||
            string.IsNullOrEmpty(userDto.Email));
    }
}
