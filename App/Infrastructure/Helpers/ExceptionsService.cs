using App.Common.Helpers;
using App.Features.Users.Common.Roles;
using Microsoft.AspNetCore.Authentication;
using System.Linq.Expressions;
using System.Security.Claims;

namespace App.Infrastructure.Helpers;

/// <summary>
/// Helper to check for exceptions to throw.
/// </summary>
public static class ExceptionsService
{
	public const int _IdBottomBoundry = 0;

	/// <summary>
	/// Throws and Logs exception when model object is null.
	/// </summary>
	/// <typeparam name="T">Model data type.</typeparam>
	/// <param name="operationName">Name of operation during exception could occured.</param>
	/// <param name="model">Object of model data.</param>
	/// <param name="modelName">Model name.</param>
	/// <param name="logger">Logger from class that invokes method.</param>
	/// <exception cref="ArgumentNullException">Occurs when model is null.</exception>
	public static void ThrowWhenModelIsNull<T>(string operationName, T? model, ILogger logger) where T : class
	{
		if (model == null)
		{
			string modelTypeName = typeof(T).Name;
			logger.LogError(MessagesPacket.LogArgumentNullOrEmpty, operationName, modelTypeName);
			throw new ArgumentNullException(modelTypeName, MessagesPacket.ProvidedObjectIsNull);
		}
	}

	/// <summary>
	/// Check is the model a null object? if it is then log and throw ArgumentNullException.
	/// </summary>
	/// <typeparam name="T">Model type.</typeparam>
	/// <param name="operationName">Name of the operation within this method is invoke.</param>
	/// <param name="model"></param>
	/// <param name="logger">Logger object of class that checks for exceptions.</param>
	/// <param name="message">Message must to contain two arguments as {operationName} and {modelTypeName}.</param>
	/// <exception cref="ArgumentNullException"></exception>
	public static void ThrowEntityNotFoundInDb(string operationName, string dataTypeName, string id, ILogger logger)
	{
		logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, dataTypeName, id);
		throw new InvalidOperationException(MessagesPacket.EntityNotFoundInDb(operationName, dataTypeName, id));
	}

	public static void ThrowGroupOfEntitiesNotFoundInDb(string operationName, string dataTypeName, ILogger logger)
	{
		logger.LogError(MessagesPacket.LogGroupOfEntitiesNotFoundInDbSet, operationName, dataTypeName);
	}

	/// <summary>
	/// Throws and Logs exception when method's argument is null.
	/// </summary>
	/// <param name="operationName"></param>
	/// <param name="argument"></param>
	/// <param name="argumentName"></param>
	/// <param name="logger"></param>
	/// <exception cref="ArgumentNullException"></exception>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	/// <exception cref="ArgumentException"></exception>
	public static void ThrowWhenArgumentIsInvalid(string operationName, object argument, string argumentName, ILogger logger)
	{
		if (argument is null)
		{
			logger.LogError(MessagesPacket.LogArgumentNullOrEmpty, operationName, argumentName);
			throw new ArgumentNullException(argumentName, MessagesPacket.ProvidedObjectIsNull);
		}

		if (argument is string idString)
		{
			if (string.IsNullOrEmpty(idString))
			{
				logger.LogError(MessagesPacket.LogArgumentNullOrEmpty, operationName, argumentName);
				throw new ArgumentNullException(argumentName, MessagesPacket.ProvidedArgumentIsNullOrEmpty);
			}
		}
		else if (argument is int idInt)
		{
			if (idInt < _IdBottomBoundry)
			{
				logger.LogError(MessagesPacket.LogOutOfRange, operationName, argumentName, argument);
				throw new ArgumentOutOfRangeException(argumentName, MessagesPacket.ProvidedArgumentIsOutOfRange);
			}
		}
		else
		{
			logger.LogError(MessagesPacket.LogInvalidArgumentType, operationName, argumentName);
			throw new ArgumentException(MessagesPacket.ProvidedArgumentIsWithInvalidType, argumentName);
		}
	}

	/// <summary>
	/// Throws and Logs exception when string param is null or empty.
	/// </summary>
	/// <param name="operationName">Name of operation during exception could occured.</param>
	/// <param name="logger">Logger from class that invokes method.</param>
	/// <param name="argument">String argument.</param>
	/// <exception cref="ArgumentOutOfRangeException">Occurs when User Id is null.</exception>
	public static void ThrowExceptionWhenArgumentIsNullOrEmpty(string operationName, string? argument, string argumentName, ILogger logger)
	{
		if (string.IsNullOrEmpty(argument))
		{
			logger.LogError(MessagesPacket.LogArgumentNullOrEmpty, operationName, argumentName);
			throw new ArgumentNullException(MessagesPacket.ExceptionErrorNullObjectOnAction(operationName, argumentName));
		}
	}

	/// <summary>
	/// Throws and Logs exception when id value is lower than bottom boundry.
	/// </summary>
	/// <param name="operationName">Name of operation during exception could occured.</param>
	/// <param name="id">Id value.</param>
	/// <param name="paramName">Name of method's param.</param>
	/// <param name="bottomBoundry">The Lower allowed value for id.</param>
	/// <param name="logger">Logger from class that invokes method.</param>
	/// <exception cref="ArgumentOutOfRangeException">Occured when id is lower than bottom boundry.</exception>
	public static void ThrowExceptionWhenIdLowerThanBottomBoundry(string operationName, int id, string paramName, ILogger logger)
	{
		if (id < _IdBottomBoundry)
		{
			logger.LogError(MessagesPacket.LogOutOfRange, operationName, paramName, id);
			throw new ArgumentOutOfRangeException(paramName, id, MessagesPacket.ArgumentOutOfRange(operationName, nameof(id), id));
		}
	}

	public static void ThrowWhenFilterExpressionIsNull<TEntity>(Expression<Func<TEntity, bool>> filter, string operationName, ILogger logger)
	{
		if (filter == null)
		{
			logger.LogError(MessagesPacket.LogFilterExpressionIsNull, operationName);
			throw new ArgumentNullException(nameof(filter), MessagesPacket.LogFilterExpressionIsNull);
		}
	}

	public static void ThrowWhenIdentityIsNull(ClaimsIdentity? identity, ILogger _logger)
	{
		if (identity == null)
		{
			_logger.LogCritical(MessagesPacket.ClaimsIdentityIsNull);
			throw new InvalidOperationException(MessagesPacket.ClaimsIdentityIsNull);
		}
	}

	public static void ThrowWhenIdsAreNotEqual(string operationName, object firstId, string firstIdName, object secondId, string secondIdName, ILogger logger)
	{
		if (firstId is null)
		{
			logger.LogCritical(MessagesPacket.LogArgumentNullOrEmpty, nameof(ThrowWhenIdsAreNotEqual), nameof(firstId));
			throw new ArgumentNullException(nameof(firstId), MessagesPacket.ProvidedArgumentIsNullOrEmpty);
		}

		if (secondId is null)
		{
			logger.LogCritical(MessagesPacket.LogArgumentNullOrEmpty, nameof(ThrowWhenIdsAreNotEqual), nameof(secondId));
			throw new ArgumentNullException(nameof(secondId), MessagesPacket.ProvidedArgumentIsNullOrEmpty);
		}

		if (firstId.ToString() != secondId.ToString())
		{
			logger.LogError(MessagesPacket.LogConflictBetweenEntitiesIds, operationName, firstId, firstIdName, secondId, secondIdName);
			throw new InvalidOperationException(MessagesPacket.ConflictBetweenEntitiesIds(operationName, firstId, firstIdName, secondId, secondIdName));
		}
	}

	public static void ThrowWhenRoleNotFoundInDb(string operationName, IRoleModel? roleModel, string roleName, ILogger logger)
	{
		if (roleModel is null)
		{
			logger.LogCritical(MessagesPacket.LogCriticalErrorRoleNotFoundInDb, operationName, roleName);
			throw new InvalidOperationException(MessagesPacket.RoleNotFoundInDb(operationName, roleName));
		}
	}

	public static void ThrowWhenAuthOptionsObjectIsNull(string operationName, AuthenticationSchemeOptions? options, string optionsTypeName, ILogger logger)
	{
		if (options is null)
		{
			logger.LogCritical(MessagesPacket.LogOptionsObjectIsNull, operationName, optionsTypeName);
			throw new InvalidOperationException(MessagesPacket.ExceptionErrorNullObjectOnAction(operationName, optionsTypeName));
		}
	}
}
