using App.Infrastructure.Databases.Common;
using App.Infrastructure.Databases.Identity.Interfaces;

namespace App.Infrastructure.Databases.Identity;

///<inheritdoc />
public class IdentityUnitOfWork : UnitOfWork<CustomIdentityDbContext>, IIdentityUnitOfWork
{
	///<inheritdoc />
	public IUserRepository UserRepository { get; }

	///<inheritdoc />
	public IRoleRepository RoleRepository { get; }

	public IdentityUnitOfWork(CustomIdentityDbContext identityContext, IUserRepository userRepository, IRoleRepository roleRepository)
		: base(identityContext)
	{
		UserRepository = userRepository;
		RoleRepository = roleRepository;
	}
}
