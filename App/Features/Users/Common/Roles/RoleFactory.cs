using App.Features.Users.Common.Roles.Interfaces;
using App.Features.Users.Common.Roles.Models;

namespace App.Features.Users.Common.Roles;

public class RoleFactory : IRoleFactory
{
    public RoleDto CreateDto()
    {
        return new RoleDto();
    }

    public RoleModel CreateModel()
    {
        return new RoleModel();
    }
}
