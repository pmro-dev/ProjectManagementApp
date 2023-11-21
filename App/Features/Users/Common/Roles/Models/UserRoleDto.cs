using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Models.Interfaces;

namespace App.Features.Users.Common.Roles.Models;

public class UserRoleDto : IUserRoleDto
{
    public RoleDto? Role { get; set; }
    public string RoleId { get; set; } = string.Empty;
    public UserDto? User { get; set; }
    public string UserId { get; set; } = string.Empty;
}
