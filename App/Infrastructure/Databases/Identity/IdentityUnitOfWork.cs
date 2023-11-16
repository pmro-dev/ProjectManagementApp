using App.Infrastructure.Databases.Common;
using App.Infrastructure.Databases.Identity.Interfaces;

namespace App.Infrastructure.Databases.Identity;

///<inheritdoc />
public class IdentityUnitOfWork : UnitOfWork<CustomIdentityDbContext>, IIdentityUnitOfWork
{
	protected readonly ILogger<IdentityUnitOfWork> _logger;

	///<inheritdoc />
	public IUserRepository UserRepository { get; }

	///<inheritdoc />
	public IRoleRepository RoleRepository { get; }

	public IdentityUnitOfWork(CustomIdentityDbContext identityContext, IUserRepository userRepository, IRoleRepository roleRepository, ILogger<IdentityUnitOfWork> logger)
		: base(identityContext, logger)
	{
		UserRepository = userRepository;
		RoleRepository = roleRepository;
		_logger = logger;
	}
}
