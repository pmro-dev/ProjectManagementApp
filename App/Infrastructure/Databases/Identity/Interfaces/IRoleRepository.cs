using App.Features.Users.Common.Roles.Models;
using App.Infrastructure.Databases.Common.Interfaces;

namespace App.Infrastructure.Databases.Identity.Interfaces;

/// <summary>
/// Role Repository allows to manage operations on User's data in Db.
/// </summary>
public interface IRoleRepository : IGenericRepository<RoleModel>
{
}
