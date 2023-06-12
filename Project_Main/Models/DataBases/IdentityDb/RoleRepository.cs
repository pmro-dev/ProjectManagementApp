using Project_IdentityDomainEntities;
using Project_Main.Models.DataBases.General;

namespace Project_Main.Models.DataBases.Identity
{
	///<inheritdoc />
	public class RoleRepository : GenericRepository<RoleModel>, IRoleRepository
    {
		///<inheritdoc />
		public RoleRepository(CustomIdentityDbContext identityContext, ILogger<RoleRepository> logger) : base(identityContext, logger) { }
    }
}
