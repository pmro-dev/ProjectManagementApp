using App.Features.Users.Common.Interfaces;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Interfaces;
using App.Features.Users.Common.Roles.Models;

namespace App.Features.Users.Common;

public class UserFactory : IUserFactory
{
    private readonly IRoleFactory _roleFactory;

    public UserFactory(IRoleFactory roleFactory)
    {
        _roleFactory = roleFactory;
    }

    public UserModel CreateModel()
	{
		return new UserModel();
	}

	public UserDto CreateDto()
	{
		return new UserDto();
	}

    public UserRoleModel CreateUserRoleModel()
    {
        return new UserRoleModel(this.CreateModel(), _roleFactory.CreateModel());
    }

    public UserRoleDto CreateUserRoleDto()
    {
        return new UserRoleDto();
	}
}
