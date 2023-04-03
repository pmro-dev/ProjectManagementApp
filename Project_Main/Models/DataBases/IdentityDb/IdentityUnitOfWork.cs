using Project_Main.Models.DataBases.General;

namespace Project_Main.Models.DataBases.Identity
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
