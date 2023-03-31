using Project_Main.Models.DataBases.Repositories.General;

namespace Project_Main.Models.DataBases.Repositories.Identity
{
    public interface IIdentityUnitOfWork : IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
    }
}
