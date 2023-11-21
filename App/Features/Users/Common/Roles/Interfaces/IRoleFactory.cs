using App.Common.Interfaces;
using App.Features.Users.Common.Roles.Models;

namespace App.Features.Users.Common.Roles.Interfaces;

public interface IRoleFactory : IBaseEntityFactory<RoleModel, RoleDto>
{
}
