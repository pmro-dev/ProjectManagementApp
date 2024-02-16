using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Models;

namespace App.Features.Users.Common.Roles.Models.Interfaces;

public interface IUserRoleDto
{
    RoleDto? Role { get; set; }
    Guid RoleId { get; set; }
    UserDto? User { get; set; }
    string UserId { get; set; }
}
