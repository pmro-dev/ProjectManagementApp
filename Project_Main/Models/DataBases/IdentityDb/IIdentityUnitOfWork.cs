using Project_Main.Models.DataBases.General;

namespace Project_Main.Models.DataBases.Identity
{
    public interface IIdentityUnitOfWork : IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
    }
}
