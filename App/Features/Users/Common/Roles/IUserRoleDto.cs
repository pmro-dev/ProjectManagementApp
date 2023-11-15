using App.Features.Users.Interfaces;

namespace App.Features.Users.Common.Roles;

public interface IUserRoleDto
{
    IRoleDto? Role { get; set; }
    string RoleId { get; set; }
    IUserDto? User { get; set; }
    string UserId { get; set; }
}
