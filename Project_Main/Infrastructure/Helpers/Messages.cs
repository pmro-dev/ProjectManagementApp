namespace Project_Main.Infrastructure.Helpers
{
    /// <summary>
    /// Helper class with const messages and methods that return formated string messages.
    /// </summary>
    public static class Messages
    {
		#region MESSAGES FOR EXCEPTIONS

		public static string ExceptionNullObjectOnAction(string actionName, string objectName)
        {
            return $"Error occured on attempt to {actionName}. Details: Object {objectName} cannot be null.";
        }

		public static string ParamObjectNull(string actionName, string objectName)
		{
			return $"{actionName} | Provided param object {objectName} cannot be null!";
		}

		public static string DbSetNull(string actionName, string dbSetName)
		{
			return $"{actionName} | DbSet - {dbSetName} cannot be null!";
		}

		public static string BuildingSucceed(string actionName, string typeName)
		{
			return $"{typeName} - {actionName} succeed!";
		}

		public static string OutOfRange(string actionName, string paramName, int paramValue)
		{
			return $"{actionName} | Given param {paramName} with value ( {paramValue} ) is out of range!";
		}

		public static string EntityNotFoundByIdInDb(string actionName, string dbSetName, int entityId)
		{
			return $"{actionName} | Entity with given id ( {entityId} ) not found in {dbSetName} DbSet.";
		}

		public static string DbContextIsNull(string dbContextName)
		{
			return $"Critical Error occured! DbContext object is null - {dbContextName}";
		}

		public static string RoleForNewUserNotFoundInDb(string roleObjectName, string roleName)
		{
			return $"Critical Error occured! Role for new user not found in Db! Object: {roleObjectName} Role: {roleName}";
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
		public const string HttpContextNullOnLogout = "Critical error occured while logging out by 3rd party provider!";

		#endregion


		#region MESSAGES FOR LOGGER

		public const string LogInvalidProviderName = "Invalid provider name from route!.";
		public const string LogExceptionOccurredOnLogging = "Error occured on attempt to login user.";
		public const string LogExceptionOccuredOnMethod = "Error {exceptionType} occured on method: {methodName}";
		public const string LogConflictBetweenIdsOfTodoListAndModelObject = "{operationName} | Conflict occured! Given id ( {todoListId} ) for To Do List and To Do List id in Model object ( {taskTodoListId} ) are not equal!";
		public const string LogConflictBetweenTodoListIdsFromTodoListModelAndTaskModel = "{operationName} | Conflict occured! To Do List id ( {taskModelTodoListId} ) from Task object and target To Do List id ( {todoListId} ) in To Do List object are not equal!";
		public const string LogErrorOnMethod = "Error errors occured on method: {methodName}";
		public const string LogTransactionFailed = "{operationName} | Transaction failed! - entity id {idValue}";
		public const string LogEntityNotFoundInDbSet = "{actionName} | Entity with given id ( {entityId} ) not found in {dbSetName} DbSet.";
		public const string LogOutOfRange = "{actionName} | Given param {paramName} with value ( {paramValue} ) is out of range!";
		public const string LogInvalidArgumentType = "{actionName} | Given param {paramName} has invalid type!";
		public const string LogBuildingSucceed = "{typeName} - {actionName} succeed!";
		public const string LogParamNullOrEmpty = "{actionName} | Provided param object {objectName} cannot be null, nor empty!";
		public const string LogDbSetNull = "{actionName} | DbSet - {dbSetName} cannot be null!";
		public const string LogErrorDbContextIsNull = "Critical Error occured! DbContext object is null - {DbContextName}";
		public const string LogNotAnyTodoListInDb = "{operationName} | There's not any To Do List in Database.";
		public const string LogCreatingUserIdentityFailed = "{operationName} | Creating new User Identity failed!";
		public const string LogCriticalErrorRoleForNewUserNotFoundInDb = "{operationName} | Object with role (from Db) for new user is null! Object: {roleObjectName} Role: {roleName}";
		public const string LoginFailedForRegisteredUser = "{operationName} | Attempt to login registered user failed! User: {username}";
		public const string UnableToAuthenticateUserPrincipal = "{operationName} | User Claims Principal is null when authenticating in {serviceName}!";
		public const string LogHttpContextNullOnLogout = "{operationName} | HttpContext is null while logging out by 3rd party provider.";

		#endregion
	}
}
