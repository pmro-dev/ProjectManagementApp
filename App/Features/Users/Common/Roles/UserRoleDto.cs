using App.Features.Users.Common.Models;

namespace App.Features.Users.Common.Roles;

public class UserRoleDto : IUserRoleDto
{
    public RoleDto? Role { get; set; }
    public string RoleId { get; set; } = string.Empty;
    public UserDto? User { get; set; }
    public string UserId { get; set; } = string.Empty;
}
