using Microsoft.EntityFrameworkCore;
using Project_IdentityDomainEntities;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.Repositories.General;

namespace Project_Main.Models.DataBases.Repositories.Identity
{
    public class UserRepository : GenericRepository<UserModel>, IUserRepository
    {
        private readonly CustomIdentityDbContext _identityContext;
        private readonly ILogger<UserRepository> _logger;
        //private string methodName = string.Empty;
        private string operationName = string.Empty;
        private readonly string repoName = nameof(UserRepository);

        public UserRepository(CustomIdentityDbContext identityContext, ILogger<UserRepository> logger) : base(identityContext, logger)
        {
            _identityContext = identityContext;
            _logger = logger;
        }

        public async Task<UserModel?> GetWithDetailsAsync(string userId)
        {
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetWithDetailsAsync), repoName);
            HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref userId, nameof(userId), _logger);

            UserModel? userWithDetailsFromDb = await _identityContext.Set<UserModel>().Where(user => user.UserId == userId).Include(user => user.UserRoles).SingleOrDefaultAsync();
            //HelperCheck.IfInstanceNullThrowException(operationName, userWithDetailsFromDb, nameof(userWithDetailsFromDb), _logger);

            return userWithDetailsFromDb;
        }

        public async Task<UserModel?> GetByNameAndPasswordAsync(string userLogin, string userPassword)
        {
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetByNameAndPasswordAsync), nameof(UserRepository));
            HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref userLogin, nameof(userLogin), _logger);
            HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref userPassword, nameof(userPassword), _logger);

            return await _identityContext.Set<UserModel>().SingleOrDefaultAsync(user => user.Username == userLogin && user.Password == userPassword);
        }

        public async Task<bool> IsNameTakenAsync(string userName)
        {
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(IsNameTakenAsync), repoName);
            bool result = false;

            result = await _identityContext.Set<UserModel>().AnyAsync(user => user.Username == userName);
            //HelperCheck.IfInstanceNullThrowException(operationName, userFromDb, nameof(userFromDb), _logger);

            return result;
        }

		public async Task<IEnumerable<RoleModel>> GetRolesAsync(string userId)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(GetRolesAsync), repoName);
			List<RoleModel> userRoles = await _identityContext.Set<UserRoleModel>()
			.Where(user => user.UserId == userId)
			.Select(user => user.Role)
			.ToListAsync();

			return userRoles;
		}

		//public async Task<bool> UpdateUserAsync(UserModel userToUpdate)
		//{
		//	operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(UpdateUserAsync), repoName);
		//	HelperCheck.IfArgumentModelNullThrowException(operationName, userToUpdate, nameof(userToUpdate), _logger);

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

		//private async Task ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(TryCatchBlockDelegateType operationsForDbTryCatchBlockAsync)
		//{
		//	methodName = operationsForDbTryCatchBlockAsync.Method.Name;

		//	try
		//	{
		//		await operationsForDbTryCatchBlockAsync.Invoke();
		//	}
		//	catch (CannotInsertNullException ex)
		//	{
		//		LogError(ex, methodName);
		//		throw;
		//	}
		//	catch (NumericOverflowException ex)
		//	{
		//		LogError(ex, methodName);
		//		throw;
		//	}
		//	catch (ReferenceConstraintException ex)
		//	{
		//		LogError(ex, methodName);
		//		throw;
		//	}
		//	catch (MaxLengthExceededException ex)
		//	{
		//		LogError(ex, methodName);
		//		throw;
		//	}
		//	catch (UniqueConstraintException ex)
		//	{
		//		LogError(ex, methodName);
		//		throw;
		//	}
		//	catch (SqlException ex)
		//	{
		//		LogError(ex, methodName);
		//		throw;
		//	}
		//	catch (InvalidOperationException ex)
		//	{
		//		LogError(ex, methodName);
		//		throw;
		//	}
		//	catch (AggregateException agg)
		//	{
		//		_logger.LogError(agg.Flatten().InnerException, Messages.ErrorOnMethodLogger, methodName);
		//		throw;
		//	}
		//}

		//private void LogError(Exception ex, string methodName)
		//{
		//	_logger.LogError(ex, Messages.ExceptionOccuredLogger, ex.GetType(), methodName);
		//}


	}
}
