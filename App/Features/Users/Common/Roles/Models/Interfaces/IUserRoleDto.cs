using App.Features.Users.Common.Models;

namespace App.Features.Users.Common.Roles.Models.Interfaces;

public interface IUserRoleDto
{
    RoleDto? Role { get; set; }
    string RoleId { get; set; }
    UserDto? User { get; set; }
    string UserId { get; set; }
}
