using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Models;
using App.Infrastructure.Databases.Common;
using App.Infrastructure.Databases.Identity.Interfaces;
using App.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Infrastructure.Databases.Identity;

///<inheritdoc />
public class UserRepository : GenericRepository<UserModel>, IUserRepository
{
	private readonly CustomIdentityDbContext _identityContext;
	private readonly ILogger<UserRepository> _logger;

	public UserRepository(CustomIdentityDbContext identityContext, ILogger<UserRepository> logger) : base(identityContext, logger)
	{
		_identityContext = identityContext;
		_logger = logger;
	}

	///<inheritdoc />
	public async Task<UserModel?> GetWithDetailsAsync(string userId)
	{
		ExceptionsService.WhenArgumentIsInvalidThrowError(nameof(GetWithDetailsAsync), userId, nameof(userId), _logger);

		UserModel? userWithDetailsFromDb = await _identityContext
			.Set<UserModel>()
			.Where(user => user.UserId == userId)
			.Include(user => user.UserRoles)
			.SingleOrDefaultAsync();

		return userWithDetailsFromDb;
	}

	///<inheritdoc />
	public async Task<UserModel?> GetByNameAndPasswordAsync(string userLogin, string userPassword)
	{
		ExceptionsService.WhenArgumentIsNullOrEmptyThrowError(nameof(GetByNameAndPasswordAsync), userLogin, nameof(userLogin), _logger);
		ExceptionsService.WhenArgumentIsNullOrEmptyThrowError(nameof(GetByNameAndPasswordAsync), userPassword, nameof(userPassword), _logger);

		Expression<Func<UserModel, bool>> userDataPredicateExpression = (UserModel user) => user.Username == userLogin && user.Password == userPassword;

		return await _identityContext
			.Set<UserModel>()
			.SingleOrDefaultAsync(userDataPredicateExpression);
	}

	///<inheritdoc />
	public async Task<bool> IsNameTakenAsync(string userName)
	{
		ExceptionsService.WhenArgumentIsInvalidThrowError(nameof(IsNameTakenAsync), userName, nameof(userName), _logger);

		bool result = await _identityContext
			.Set<UserModel>()
			.AnyAsync(user => user.Username == userName);

		return result;
	}

	public async Task<bool> IsAccountExistedAsync(string userEmail)
	{
		ExceptionsService.WhenArgumentIsInvalidThrowError(nameof(IsAccountExistedAsync), userEmail, nameof(userEmail), _logger);

		bool result = await _identityContext
			.Set<UserModel>()
			.AnyAsync(user => user.Email == userEmail);

		return result;
	}

	///<inheritdoc />
	public async Task<ICollection<RoleModel>> GetRolesAsync(string userId)
	{
		ExceptionsService.WhenArgumentIsInvalidThrowError(nameof(GetRolesAsync), userId, nameof(userId), _logger);

		ICollection<RoleModel> userRoles = await _identityContext
			.Set<UserRoleModel>()
			.Where(user => user.UserId == userId)
			.Select(user => user.Role)
			.ToListAsync();

		return userRoles;
	}


	//TODO implement Update feature
	//public async Task<bool> UpdateUserAsync(UserModel userToUpdate)
	//{
	//	operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(UpdateUserAsync), repoName);
	//	HelperCheck.IfArgumentNullThrowException(operationName, userToUpdate, nameof(userToUpdate), _logger);

	//	TryCatchBlockDelegateType operationsForDbTryCatchBlockAsync = new(async () =>
	//	{
	//		UserModel? userToUpdateFromDb = await _identityContext.GetUserWithDetailsAsync(userToUpdate.UserId);
	//		//HelperCheck.IfInstanceNullThrowException(operationName, userToUpdateFromDb, nameof(userToUpdateFromDb), _logger);

	//		userToUpdateFromDb.Username = userToUpdate.Username;
	//		userToUpdateFromDb.Password = userToUpdate.Password;
	//		userToUpdateFromDb.Email = userToUpdate.Email;
	//		userToUpdateFromDb.FirstName = userToUpdate.FirstName;
	//		userToUpdateFromDb.Lastname = userToUpdate.Lastname;
	//		userToUpdateFromDb.Provider = userToUpdate.Provider;
	//		userToUpdateFromDb.NameIdentifier = userToUpdate.NameIdentifier;

	//		if (!userToUpdate.UserRoles.Any())
	//		{
	//			userToUpdateFromDb.UserRoles = userToUpdate.UserRoles;
	//		}

	//		await _identityContext.UpdateUserAsync(userToUpdateFromDb);
	//	});

	//	await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(operationsForDbTryCatchBlockAsync);
	//	return true;
	//}
}
