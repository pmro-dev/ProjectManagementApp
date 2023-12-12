using App.Features.Users.Common.Roles.Models.Interfaces;
using Microsoft.AspNetCore.Authentication;
using System.Linq.Expressions;
using System.Security.Claims;

namespace App.Features.Exceptions.Throw;

/// <summary>
/// Helper to check for exceptions to throw.
/// </summary>
public static class ExceptionsService
{
	public const int IdBottomBoundry = 0;
	public const int BoundryValueOne = 1;

	public static void WhenRoleNotFoundInDbThrow(string operationName, IRoleModel? roleModel, string roleName, ILogger logger)
	{
		if (roleModel is null)
		{
			logger.LogCritical(ExceptionsMessages.LogCriticalErrorRoleNotFoundInDb, operationName, roleName);
			throw new InvalidOperationException(ExceptionsMessages.RoleNotFoundInDb(operationName, roleName));
		}
	}

	public static void WhenAuthOptionsIsNullThrow(string operationName, AuthenticationSchemeOptions? options, string optionsTypeName, ILogger logger)
	{
		if (options is null)
		{
			logger.LogCritical(ExceptionsMessages.LogOptionsObjectIsNull, operationName, optionsTypeName);
			throw new InvalidOperationException(ExceptionsMessages.ExceptionCriticalNullObject(operationName, optionsTypeName));
		}
	}

	public static void WhenFilterExpressionIsNullThrow<TEntity>(Expression<Func<TEntity, bool>> filter, string operationName, ILogger logger)
	{
		if (filter == null)
		{
			logger.LogError(ExceptionsMessages.LogFilterExpressionIsNull, operationName);
			throw new ArgumentNullException(nameof(filter), ExceptionsMessages.LogFilterExpressionIsNull);
		}
	}

	public static void WhenGroupOfRequiredEntitiesNotFoundInDb<T>(string operationName, ICollection<T> groupOfEntities, ILogger logger)
	{
		if (!groupOfEntities.Any())
		{
			string dataTypeName = typeof(ICollection<T>).Name;
			logger.LogError(ExceptionsMessages.LogGroupOfEntitiesNotFoundInDbSet, operationName, dataTypeName);
			throw new InvalidOperationException(ExceptionsMessages.GroupOfEntitiesNotFoundInDbSet(operationName, dataTypeName));
		}
	}

	/// <summary>
	/// Throws and Logs exception when model object is null.
	/// </summary>
	/// <typeparam name="T">Model data type.</typeparam>
	/// <param name="operationName">Name of operation during exception could occured.</param>
	/// <param name="model">Object of model data.</param>
	/// <param name="modelName">Model name.</param>
	/// <param name="logger">Logger from class that invokes method.</param>
	/// <exception cref="ArgumentNullException">Occurs when model is null.</exception>
	public static void WhenEntityIsNullThrowCritical<T>(string operationName, T? model, ILogger logger, object? entityId = null)
	{
		string modelTypeName = typeof(T).Name;

		if (model is null && entityId is null)
		{
			logger.LogCritical(ExceptionsMessages.LogCriticalModelObjectIsNull, operationName, modelTypeName);
			throw new ArgumentNullException(ExceptionsMessages.ExceptionCriticalNullObject(operationName, modelTypeName));
		}
		else if (model is null)
		{
			string? entityIdAsString = Convert.ToString(entityId!);

			if (entityIdAsString is null)
			{
				logger.LogCritical(ExceptionsMessages.LogInvalidEntityIdCast, operationName, nameof(WhenEntityIsNullThrowCritical));
				throw new InvalidCastException(ExceptionsMessages.InvalidEntityIdCast(operationName, nameof(WhenEntityIsNullThrowCritical)));
			}

			logger.LogCritical(ExceptionsMessages.LogEntityNotFoundInDbSet, operationName, modelTypeName, entityIdAsString);
			throw new ArgumentNullException(ExceptionsMessages.EntityNotFoundInDb(operationName, modelTypeName, entityIdAsString));
		}
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
	public static void WhenArgumentIsInvalidThrowError(string operationName, object argument, string argumentName, ILogger logger)
	{
		if (argument is null)
		{
			logger.LogError(ExceptionsMessages.LogArgumentIsNullOrEmpty, operationName, argumentName);
			throw new ArgumentNullException(argumentName, ExceptionsMessages.ProvidedObjectIsNull);
		}

		if (argument is string idString)
		{
			if (string.IsNullOrEmpty(idString))
			{
				logger.LogError(ExceptionsMessages.LogArgumentIsNullOrEmpty, operationName, argumentName);
				throw new ArgumentNullException(argumentName, ExceptionsMessages.ProvidedArgumentIsNullOrEmpty);
			}
		}
		else if (argument is int idInt)
		{
			if (idInt < IdBottomBoundry)
			{
				logger.LogError(ExceptionsMessages.LogOutOfRange, operationName, argumentName, argument);
				throw new ArgumentOutOfRangeException(argumentName, ExceptionsMessages.ProvidedArgumentIsOutOfRange);
			}
		}
		else
		{
			logger.LogError(ExceptionsMessages.LogInvalidArgumentType, operationName, argumentName);
			throw new ArgumentException(ExceptionsMessages.ProvidedArgumentIsWithInvalidType, argumentName);
		}
	}

	/// <summary>
	/// Throws and Logs exception when string param is null or empty.
	/// </summary>
	/// <param name="operationName">Name of operation during exception could occured.</param>
	/// <param name="logger">Logger from class that invokes method.</param>
	/// <param name="argument">String argument.</param>
	/// <exception cref="ArgumentOutOfRangeException">Occurs when User Id is null.</exception>
	public static void WhenArgumentIsNullOrEmptyThrow(string operationName, string? argument, string argumentOrTypeName, ILogger logger)
	{
		if (string.IsNullOrEmpty(argument))
		{
			logger.LogError(ExceptionsMessages.LogArgumentIsNullOrEmpty, operationName, argumentOrTypeName);
			throw new ArgumentNullException(ExceptionsMessages.ExceptionCriticalNullObject(operationName, argumentOrTypeName));
		}
	}

	public static void WhenPropertyIsNullOrEmptyThrow(string operationName, string? property, string propertyOrTypeName, ILogger logger)
	{
		if (string.IsNullOrEmpty(property))
		{
			logger.LogCritical(ExceptionsMessages.LogPropertyIsNullOrEmpty, operationName, propertyOrTypeName);
			throw new InvalidOperationException(ExceptionsMessages.PropertyIsNullOrEmpty(operationName, propertyOrTypeName));
		}
	}

	/// <summary>
	/// Throws and Logs exception when id value is lower than bottom boundry.
	/// </summary>
	/// <param name="operationName">Name of operation during exception could occured.</param>
	/// <param name="logger">Logger from class that invokes method.</param>
	/// <exception cref="ArgumentOutOfRangeException">Occured when id is lower than bottom boundry.</exception>
	public static void WhenValueLowerThanBottomBoundryThrow(string operationName, int value, string paramName, ILogger logger)
	{
		if (IsValueLessThanBottomBoundry(value, DefaultBottomBoundry))
		{
			logger.LogError(ExceptionsMessages.LogOutOfRange, operationName, paramName, value);
			throw new ArgumentOutOfRangeException(paramName, value, ExceptionsMessages.ArgumentOutOfRange(operationName, nameof(value), value));
		}
	}
		{
			logger.LogError(ExceptionsMessages.LogOutOfRange, operationName, paramName, value);
			throw new ArgumentOutOfRangeException(paramName, value, ExceptionsMessages.ArgumentOutOfRange(operationName, nameof(value), value));
		}
	}

	public static void WhenIdentityIsNullThrowCritical(ClaimsIdentity? identity, ILogger _logger)
	{
		if (identity == null)
		{
			_logger.LogCritical(ExceptionsMessages.LogClaimsIdentityIsNull);
			throw new InvalidOperationException(ExceptionsMessages.LogClaimsIdentityIsNull);
		}
	}

	public static void WhenPrincipalIsNullThrowCritical(string operationName, ClaimsPrincipal? principal, ILogger _logger)
	{
		if (principal == null)
		{
			_logger.LogCritical(ExceptionsMessages.LogCriticalUserPrincipalIsNull, operationName);
			throw new InvalidOperationException(ExceptionsMessages.CriticalUserPrincipalIsNull(operationName));
		}
	}

	public static void WhenIdsAreNotEqualThrowCritical(string operationName, object firstId, string firstIdName, object secondId, string secondIdName, ILogger logger)
	{
		if (firstId is null)
		{
			logger.LogCritical(ExceptionsMessages.LogArgumentIsNullOrEmpty, nameof(WhenIdsAreNotEqualThrowCritical), nameof(firstId));
			throw new ArgumentNullException(nameof(firstId), ExceptionsMessages.ProvidedArgumentIsNullOrEmpty);
		}

		if (secondId is null)
		{
			logger.LogCritical(ExceptionsMessages.LogArgumentIsNullOrEmpty, nameof(WhenIdsAreNotEqualThrowCritical), nameof(secondId));
			throw new ArgumentNullException(nameof(secondId), ExceptionsMessages.ProvidedArgumentIsNullOrEmpty);
		}

		if (firstId.ToString() != secondId.ToString())
		{
			logger.LogCritical(ExceptionsMessages.LogConflictBetweenEntitiesIds, operationName, firstId, firstIdName, secondId, secondIdName);
			throw new InvalidOperationException(ExceptionsMessages.ConflictBetweenEntitiesIds(operationName, firstId, firstIdName, secondId, secondIdName));
		}
	}

	private static bool IsValueLessThanBottomBoundry(int value, int bottomBoundry)
	{
		if (value < bottomBoundry)
			return true;

		return false;
	}
}
