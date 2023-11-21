using App.Features.Users.Common.Roles.Models.Interfaces;

namespace App.Features.Users.Common.Roles.Models;

public class RoleDto : IRoleDto
{
    public string DataVersion { get; set; } = DateTime.Now.ToString();
    public string Description { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public ICollection<IUserRoleDto> UserRoles { get; set; } = new List<IUserRoleDto>();
}
