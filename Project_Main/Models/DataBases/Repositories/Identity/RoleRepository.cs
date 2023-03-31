using Project_IdentityDomainEntities;
using Project_Main.Models.DataBases.Repositories.General;

namespace Project_Main.Models.DataBases.Repositories.Identity
{
	public class RoleRepository : GenericRepository<RoleModel>, IRoleRepository
	{
		public RoleRepository(CustomIdentityDbContext identityContext, ILogger<RoleRepository> logger) : base(identityContext, logger) {}
	}
}
