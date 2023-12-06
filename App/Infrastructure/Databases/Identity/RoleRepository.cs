using App.Features.Users.Common.Roles.Models;
using App.Infrastructure.Databases.Common;
using App.Infrastructure.Databases.Identity.Interfaces;

namespace App.Infrastructure.Databases.Identity;

///<inheritdoc />
public class RoleRepository : GenericRepository<RoleModel, string>, IRoleRepository
{
	///<inheritdoc />
	public RoleRepository(CustomIdentityDbContext identityContext, ILogger<RoleRepository> logger) : base(identityContext, logger) { }
}
