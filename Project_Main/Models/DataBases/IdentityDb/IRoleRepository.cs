using Project_IdentityDomainEntities;
using Project_Main.Models.DataBases.General;

namespace Project_Main.Models.DataBases.Identity
{
	/// <summary>
	/// Role Repository allows to manage operations on User's data in Db.
	/// </summary>
	public interface IRoleRepository : IGenericRepository<RoleModel>
    {
    }
}
