namespace App.Common.Helpers;

/// <summary>
/// Helper class with const messages and methods that return formated string messages.
/// </summary>
public static class MessagesPacket
{
	#region MESSAGES FOR EXCEPTIONS

	public static string ExceptionErrorNullObjectOnAction(string operationName, string objectTypeName)
	{
		return $"Error occured on attempt to {operationName}. Details: Object {objectTypeName} cannot be null.";
	}

	public static string ArgumentOutOfRange(string operationName, string argumentName, int argumentValue)
	{
		return $"{operationName} | Given argument {argumentName} with value ( {argumentValue} ) is out of range!";
	}

	public static string EntityNotFoundInDb(string operationName, string dataTypeName, string entityId)
	{
		return $"{operationName} | {dataTypeName} Entity with given id ( {entityId} ) not found in DbSet.";
	}

	public static string RoleNotFoundInDb(string operationName, string roleName)
	{
		return $"{operationName} | Role ( {roleName} ) not found in a database.";
	}

	public static string HttpContextObjectIsNull(string operationName)
	{
		return $"{operationName} | Unable to get HttpContext - accessor returned null object.";
	}

	public static string ConflictBetweenEntitiesIds(string operationName, object firstId, string firstIdName, object secondId, string secondIdName)
	{
		return $"{operationName} | Entities Ids Conflict! Given id ( {firstId} ) for {firstIdName} and ( {secondId} ) for {secondIdName} are not equal!";
	}

	public const string ProvidedObjectIsNull = "Critical error! Provided object cannot be null!";
	public const string ProvidedArgumentIsNullOrEmpty = "Passed argument cannot be null or empty!";
	public const string ProvidedArgumentIsOutOfRange = "Passed argument is out of range!";
	public const string ProvidedArgumentIsWithInvalidType = "The id parameter is not a valid type.";
	public const string DbSetIsNull = "Critical error! Dbset object cannot be null!";
	public const string ItemNotFoundInDb = "There's not such item in Database.";
	public const string InvalidLoginData = "Invalid name or password.";
	public const string UnableToLogin = "Unable to login, try again or contact support";
	public const string InvalidRegisterData = "Invalid register data: login, password or email!";
	public const string NameTaken = "Name already taken! Try again and get new one!";

	#endregion


	#region MESSAGES FOR LOGGER

	public const string LogInvalidProviderName = "Invalid provider name from route!.";
	public const string LogExceptionOccurredOnLogging = "Error occured on attempt to login user.";
	public const string LogExceptionOccuredOnMethod = "Error {exceptionType} occured on method: {methodName}";
	public const string LogConflictBetweenEntitiesIds = "{operationName} | Entities Ids Conflict! Given id ( {firstId} ) for {firstIdName} and ( {secondId} ) for {secondIdName} are not equal!";
	public const string LogConflictBetweenTodoListIdsFromTodoListModelAndTaskModel = "{operationName} | Conflict occured! To Do List id ( {taskModelTodoListId} ) from Task object and target To Do List id ( {todoListId} ) in To Do List object are not equal!";
	public const string LogErrorOnMethod = "Error errors occured on method: {methodName}";
	public const string LogTransactionFailed = "{operationName} | Transaction failed! - entity id {idValue}";
	public const string LogEntityNotFoundInDbSet = "{actionName} | {entityTypeName} Entity with given id ( {entityId} ) not found in DbSet.";
	public const string LogGroupOfEntitiesNotFoundInDbSet = "{actionName} | Group of {entityTypeName} Entities not found in DbSet.";
	public const string LogOutOfRange = "{actionName} | Given param {paramName} with value ( {paramValue} ) is out of range!";
	public const string LogInvalidArgumentType = "{actionName} | Given param {paramName} has invalid type!";
	public const string LogBuildingSucceed = "{typeName} - {actionName} succeed!";
	public const string LogArgumentNullOrEmpty = "{actionName} | Provided argument ( name: {objectName} ) cannot be null, nor empty!";
	public const string LogDbSetNull = "{actionName} | DbSet - {dbSetName} cannot be null!";
	public const string LogErrorDbContextIsNull = "Critical Error occured! DbContext object is null - {DbContextName}";
	public const string LogNotAnyTodoListInDb = "{operationName} | There's not any To Do List in Database.";
	public const string LogCreatingUserIdentityFailed = "{operationName} | Creating new User Identity failed!";
	public const string LogCriticalErrorRoleNotFoundInDb = "{operationName} | Role ( {roleName} ) not found in a database.";
	public const string LoginFailedForRegisteredUser = "{operationName} | Attempt to login registered user failed! User: {username}";
	public const string UnableToAuthenticateUserPrincipal = "{operationName} | User Claims Principal is null when authenticating in {serviceName}!";
	public const string LogHttpContextObjectIsNull = "{operationName} | Unable to get HttpContext - accessor returned null object.";
	public const string LogFilterExpressionIsNull = "{operationName} | Given filter expression is set to null which does not allow to proceed operation.";
	public const string LogExceptionOccuredOnSavingDataToDataBase = "{operationName} | Exception occured on attempt to save data to database!";
	public const string LogRollbackProceed = "Rollback proceed!";
	public const string LogExceptionOccuredOnCommitingTransaction = "{operationName} | Exception occured on attempt to commit transaction to database!";
	public const string SeedCollectionsAreEmpty = "Some Seed collections are empty!";
	public const string ClaimsIdentityIsNull = "Unable to proceed with signing in user because of null value of claims identity!";
	public const string LogOptionsObjectIsNull = "{operationName} | Used options ( {optionsTypeName} ) object was null!";

	#endregion
}
