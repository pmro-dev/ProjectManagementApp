namespace App.Features.Users.Common.Roles.Models.Interfaces;

public interface IRoleDto
{
    byte[] RowVersion { get; set; }
    string Description { get; set; }
    Guid Id { get; set; }
    string Name { get; set; }
    ICollection<IUserRoleDto> UserRoles { get; set; }
}
