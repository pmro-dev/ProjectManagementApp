using Microsoft.EntityFrameworkCore;
using Web.Accounts.Users.Interfaces;
using Web.Databases.Common;
using Web.Databases.Identity;
using Web.Infrastructure.Helpers;

namespace Web.Accounts.Users.Common
{
	///<inheritdoc />
	public class UserRepository : GenericRepository<UserModel>, IUserRepository
	{
		private readonly CustomIdentityDbContext _identityContext;
		private readonly ILogger<UserRepository> _logger;
		private string operationName = string.Empty;
		private readonly string repoName = nameof(UserRepository);

		public UserRepository(CustomIdentityDbContext identityContext, ILogger<UserRepository> logger) : base(identityContext, logger)
		{
			_identityContext = identityContext;
			_logger = logger;
		}

		///<inheritdoc />
		public async Task<IUserModel?> GetWithDetailsAsync(string userId)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetWithDetailsAsync), repoName);
			HelperCheck.ThrowExceptionWhenParamNullOrEmpty(operationName, ref userId, nameof(userId), _logger);

			UserModel? userWithDetailsFromDb = await _identityContext
				.Set<UserModel>()
				.Where(user => user.UserId == userId)
				.Include(user => user.UserRoles)
				.SingleOrDefaultAsync();

			return userWithDetailsFromDb;
		}

		///<inheritdoc />
		public async Task<IUserModel?> GetByNameAndPasswordAsync(string userLogin, string userPassword)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetByNameAndPasswordAsync), nameof(UserRepository));
			HelperCheck.ThrowExceptionWhenParamNullOrEmpty(operationName, ref userLogin, nameof(userLogin), _logger);
			HelperCheck.ThrowExceptionWhenParamNullOrEmpty(operationName, ref userPassword, nameof(userPassword), _logger);

			return await _identityContext
				.Set<UserModel>()
				.SingleOrDefaultAsync(user => user.Username == userLogin && user.Password == userPassword);
		}

		///<inheritdoc />
		public async Task<bool> IsNameTakenAsync(string userName)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(IsNameTakenAsync), repoName);
			bool result = await _identityContext
				.Set<UserModel>()
				.AnyAsync(user => user.Username == userName);

			return result;
		}

		///<inheritdoc />
		public async Task<ICollection<IRoleModel>> GetRolesAsync(string userId)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetRolesAsync), repoName);
			ICollection<IRoleModel> userRoles = await _identityContext
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
}
