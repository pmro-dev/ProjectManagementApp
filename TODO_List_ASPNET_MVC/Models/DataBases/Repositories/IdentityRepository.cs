using EntityFramework.Exceptions.Common;
using Project_IdentityDomainEntities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.IdentityDb;

namespace Project_Main.Models.DataBases.Repositories
{
	public class IdentityRepository : IIdentityRepository
	{
		private readonly IdentityDbContext _identityContext;
		private readonly ILogger<IdentityRepository> _logger;
		delegate Task TryCatchBlockDelegateType();
		private string methodName = string.Empty;
		private string operationName = string.Empty;
		private readonly string contextName = nameof(IdentityRepository);

		public IdentityRepository(IdentityDbContext identityContext, ILogger<IdentityRepository> logger)
		{
			_identityContext = identityContext;
			_logger = logger;
		}

		public async Task<bool> AddUserAsync(UserModel newUser)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(AddUserAsync), contextName);
			HelperCheck.IfArgumentModelNullThrowException(operationName, newUser, nameof(newUser), _logger);

			TryCatchBlockDelegateType operationsForDbTryCatchBlockAsync = new(async () =>
			{
				await _identityContext.AddUserAsync(newUser);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(operationsForDbTryCatchBlockAsync);
			return true;
		}

		public async Task<bool> DeleteUserAsync(string userId)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(DeleteUserAsync), contextName);
			HelperCheck.IFParamNullOrEmptyThrowException(operationName, ref userId, nameof(userId), _logger);

			TryCatchBlockDelegateType operationsForDbTryCatchBlockAsync = new(async () =>
			{
				UserModel? userToDelete = await _identityContext.GetUserWithDetailsAsync(userId);
				//HelperCheck.IfInstanceNullThrowException(operationName, userToDelete, nameof(userToDelete), _logger);

				await _identityContext.DeleteUserAsync(userToDelete);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(operationsForDbTryCatchBlockAsync);
			return true;
		}

		public async Task<UserModel> GetUserAsync(string userId)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetUserAsync), contextName);
			UserModel userFromDb = new();

			TryCatchBlockDelegateType operationsForDbTryCatchBlockAsync = new(async () =>
			{
				userFromDb = await _identityContext.GetUserAsync(userId);
				//HelperCheck.IfInstanceNullThrowException(operationName, userFromDb, nameof(userFromDb), _logger);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(operationsForDbTryCatchBlockAsync);
			return userFromDb;
		}

		public async Task<UserModel> GetUserWithDetailsAsync(string userId)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetUserAsync), contextName);
			UserModel userWithDetailsFromDb = new();

			TryCatchBlockDelegateType operationsForDbTryCatchBlockAsync = new(async () =>
			{
				userWithDetailsFromDb = await _identityContext.GetUserWithDetailsAsync(userId);
				//HelperCheck.IfInstanceNullThrowException(operationName, userWithDetailsFromDb, nameof(userWithDetailsFromDb), _logger);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(operationsForDbTryCatchBlockAsync);
			return userWithDetailsFromDb;
		}

		public async Task<bool> UpdateUserAsync(UserModel userToUpdate)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(UpdateUserAsync), contextName);
			HelperCheck.IfArgumentModelNullThrowException(operationName, userToUpdate, nameof(userToUpdate), _logger);

			TryCatchBlockDelegateType operationsForDbTryCatchBlockAsync = new(async () =>
			{
				UserModel? userToUpdateFromDb = await _identityContext.GetUserWithDetailsAsync(userToUpdate.UserId);
				//HelperCheck.IfInstanceNullThrowException(operationName, userToUpdateFromDb, nameof(userToUpdateFromDb), _logger);

				userToUpdateFromDb.Username = userToUpdate.Username;
				userToUpdateFromDb.Password = userToUpdate.Password;
				userToUpdateFromDb.Email = userToUpdate.Email;
				userToUpdateFromDb.FirstName = userToUpdate.FirstName;
				userToUpdateFromDb.Lastname = userToUpdate.Lastname;
				userToUpdateFromDb.Provider = userToUpdate.Provider;
				userToUpdateFromDb.NameIdentifier = userToUpdate.NameIdentifier;

				if (!userToUpdate.UserRoles.Any())
				{
					userToUpdateFromDb.UserRoles = userToUpdate.UserRoles;
				}

				await _identityContext.UpdateUserAsync(userToUpdateFromDb);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(operationsForDbTryCatchBlockAsync);
			return true;
		}

		private async Task ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(TryCatchBlockDelegateType operationsForDbTryCatchBlockAsync)
		{
			methodName = operationsForDbTryCatchBlockAsync.Method.Name;

			try
			{
				await operationsForDbTryCatchBlockAsync.Invoke();
			}
			catch (CannotInsertNullException ex)
			{
				LogError(ex, methodName);
				throw;
			}
			catch (NumericOverflowException ex)
			{
				LogError(ex, methodName);
				throw;
			}
			catch (ReferenceConstraintException ex)
			{
				LogError(ex, methodName);
				throw;
			}
			catch (MaxLengthExceededException ex)
			{
				LogError(ex, methodName);
				throw;
			}
			catch (UniqueConstraintException ex)
			{
				LogError(ex, methodName);
				throw;
			}
			catch (SqlException ex)
			{
				LogError(ex, methodName);
				throw;
			}
			catch (InvalidOperationException ex)
			{
				LogError(ex, methodName);
				throw;
			}
			catch (AggregateException agg)
			{
				_logger.LogError(agg.Flatten().InnerException, Messages.ErrorOnMethodLogger, methodName);
				throw;
			}
		}

		private void LogError(Exception ex, string methodName)
		{
			_logger.LogError(ex, Messages.ExceptionOccuredLogger, ex.GetType(), methodName);
		}

		public async Task<UserModel> GetUserForLoggingAsync(string userName, string userPassword)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(GetUserForLoggingAsync), contextName);

			UserModel userFromDb = new();

			TryCatchBlockDelegateType operationsForDbTryCatchBlockAsync = new(async () =>
			{
				userFromDb = await _identityContext.GetUserByNameAndPasswordAsync(userName, userPassword);
				//HelperCheck.IfInstanceNullThrowException(operationName, userFromDb, nameof(userFromDb), _logger);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(operationsForDbTryCatchBlockAsync);
			return userFromDb;
		}

		public async Task<bool> IsUserNameUsedAsync(string userName)
		{
			operationName = HelperOther.GetActionNameForLoggingAndExceptions(nameof(IsUserNameUsedAsync), contextName);

			bool result = false;

			TryCatchBlockDelegateType operationsForDbTryCatchBlockAsync = new(async () =>
			{
				result = await _identityContext.Users.AnyAsync(u => u.Username == userName);
				//HelperCheck.IfInstanceNullThrowException(operationName, userFromDb, nameof(userFromDb), _logger);
			});

			await ExecuteInTryCatchBlockToCatchEFCoreAndSQLExceptionsAsync(operationsForDbTryCatchBlockAsync);
			return result;
		}
	}
}
