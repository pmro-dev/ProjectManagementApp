using Web.Accounts.Users.Interfaces;
using Web.Databases.Common;
using Web.Databases.Identity;

namespace Web.Accounts.Users.Common
{
	///<inheritdoc />
	public class RoleRepository : GenericRepository<RoleModel>, IRoleRepository
	{
		///<inheritdoc />
		public RoleRepository(CustomIdentityDbContext identityContext, ILogger<RoleRepository> logger) : base(identityContext, logger) { }
	}
}
