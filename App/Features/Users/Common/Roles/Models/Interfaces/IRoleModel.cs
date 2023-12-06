using App.Infrastructure.Databases.Common.Interfaces;

namespace App.Features.Users.Common.Roles.Models.Interfaces;

public interface IRoleModel : IBaseEntity<string>
{
    string DataVersion { get; set; }
    string Description { get; set; }
    string Name { get; set; }
    ICollection<UserRoleModel> UserRoles { get; set; }
}