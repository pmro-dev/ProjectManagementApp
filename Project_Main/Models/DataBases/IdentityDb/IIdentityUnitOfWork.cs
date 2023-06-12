using Project_Main.Models.DataBases.General;

namespace Project_Main.Models.DataBases.Identity
{
    /// <summary>
    /// Extensions for Identity's Unit Of Work.
    /// </summary>
    public interface IIdentityUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// User Repository allows to manage Users data in Db.
        /// </summary>
        IUserRepository UserRepository { get; }

		/// <summary>
		/// Role Repository allows to manage Roles data in Db.
		/// </summary>
		IRoleRepository RoleRepository { get; }
    }
}
