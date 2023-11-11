using Web.Accounts.Users.Interfaces;
using Web.Databases.Common;
using Web.Databases.Identity.Interfaces;

namespace Web.Databases.Identity
{
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
}
