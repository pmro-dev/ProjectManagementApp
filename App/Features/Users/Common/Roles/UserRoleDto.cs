using App.Features.Users.Interfaces;

namespace App.Features.Users.Common.Roles;

public class UserRoleDto : IUserRoleDto
{
    public IRoleDto? Role { get; set; }
    public string RoleId { get; set; } = string.Empty;
    public IUserDto? User { get; set; }
    public string UserId { get; set; } = string.Empty;
}
