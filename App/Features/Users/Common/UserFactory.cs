using App.Features.Users.Common.Models;
using App.Features.Users.Interfaces;

namespace App.Features.Users.Common;

public class UserFactory : IUserFactory
{
	public UserModel CreateModel()
	{
		return new UserModel();
	}

	public UserDto CreateDto()
	{
		return new UserDto();
	}
}
