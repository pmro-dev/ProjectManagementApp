using Project_Main.Models.DataBases.Repositories.General;

namespace Project_Main.Models.DataBases.Repositories.Identity
{
    public class IdentityUnitOfWork : UnitOfWork<CustomIdentityDbContext>, IIdentityUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }

        public IdentityUnitOfWork(CustomIdentityDbContext identityContext, IUserRepository userRepository, IRoleRepository roleRepository)
            : base(identityContext)
        {
            UserRepository = userRepository;
            RoleRepository = roleRepository;
        }
    }
}
