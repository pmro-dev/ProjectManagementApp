using App.Features.Users.Common.Roles.Models.Interfaces;

namespace App.Features.Users.Common.Roles.Models;

public class RoleDto : IRoleDto
{
    public byte[] RowVersion { get; set; } = { 1, 1, 1 };
    public string Description { get; set; } = string.Empty;
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public ICollection<IUserRoleDto> UserRoles { get; set; } = new List<IUserRoleDto>();
}
