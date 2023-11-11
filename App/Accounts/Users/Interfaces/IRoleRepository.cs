using Web.Databases.Common.Interfaces;

namespace Web.Accounts.Users.Interfaces;

/// <summary>
/// Role Repository allows to manage operations on User's data in Db.
/// </summary>
public interface IRoleRepository : IGenericRepository<RoleModel>
{
}
