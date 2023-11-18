using App.Features.Users.Common.Models;

namespace App.Features.Users.Common;

public static class UserDtoValidator
{
	public static bool ValidData(UserDto userDto)
	{
		return !(string.IsNullOrEmpty(userDto.Username) ||
			string.IsNullOrEmpty(userDto.FirstName) ||
			string.IsNullOrEmpty(userDto.LastName) ||
			string.IsNullOrEmpty(userDto.Password) ||
			string.IsNullOrEmpty(userDto.Email));
	}
}
