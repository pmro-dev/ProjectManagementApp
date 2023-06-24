using Project_IdentityDomainEntities;
using Project_Main.Models.DataBases.General;

namespace Project_Main.Models.DataBases.Identity
{
    /// <summary>
    /// User Repository allows to manage operations on User's data in Db.
    /// </summary>
    public interface IUserRepository : IGenericRepository<UserModel>
    {
		/// <summary>
		/// Get a specific User with details (where details are related data in other tables).
		/// </summary>
		/// <param name="id">Targeted User id.</param>
		/// <returns>User with details from Db.</returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		/// <exception cref="OperationCanceledException"></exception>
		Task<UserModel?> GetWithDetailsAsync(string userId);

		/// <summary>
		/// Get a specific User by User Login and Password from Db.
		/// </summary>
		/// <param name="userLogin">User Login.</param>
		/// <param name="userPassword">User Password to account.</param>
		/// <returns>User from Db.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		/// <exception cref="OperationCanceledException"></exception>
		Task<UserModel?> GetByNameAndPasswordAsync(string userLogin, string userPassword);

		/// <summary>
		/// Check that any UserLogin with the same name already exists.
		/// </summary>
		/// <param name="userName">Targeted name to check.</param>
		/// <returns>True when User with specified name already exists, otherwise false.</returns>
		Task<bool> IsNameTakenAsync(string userName);

        /// <summary>
        /// Get roles of a specific user.
        /// </summary>
        /// <param name="userId">Targeted User id.</param>
        /// <returns>Set of roles of targeted User.</returns>
		Task<IEnumerable<RoleModel>> GetRolesAsync(string userId);
    }
}
