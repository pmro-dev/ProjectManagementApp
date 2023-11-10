using Project_IdentityDomainEntities;

namespace Domain.Interfaces.ForRepositories;

/// <summary>
/// Role Repository allows to manage operations on User's data in Db.
/// </summary>
public interface IRoleRepository : IGenericRepository<RoleModel>
{
}
