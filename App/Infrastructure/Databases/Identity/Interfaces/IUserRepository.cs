using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Models;
using App.Infrastructure.Databases.Common.Interfaces;

namespace App.Infrastructure.Databases.Identity.Interfaces;

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
	/// Check that any user with same email already exists.
	/// </summary>
	/// <param name="userEmail">User email to check.</param>
	/// <returns>True when account for such email already exists, false otherwise.</returns>
	Task<bool> IsAccountExistedAsync(string userEmail);


	/// <summary>
	/// Get roles of a specific user.
	/// </summary>
	/// <param name="userId">Targeted User id.</param>
	/// <returns>Set of roles of targeted User.</returns>
	Task<ICollection<RoleModel>> GetRolesAsync(string userId);
}
