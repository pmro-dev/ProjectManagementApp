namespace TODO_List_ASPNET_MVC.Infrastructure.Helpers
{
    /// <summary>
    /// Helper class with const messages and methods that return formated messages that could be used in different places in a solution.
    /// </summary>
    public static class Messages
    {
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

		public static string BuildingSucced(string actionName, string typeName)
		{
			return $"{typeName} - {actionName} succeed!";
		}

		public static string OutOfRange(string actionName, string paramName, int paramValue)
		{
			return $"{actionName} | Given param {paramName} with value ( {paramValue} ) is out of range!";
		}

		public static string EntityNotFoundInDb(string actionName, string dbSetName, int entityId)
		{
			return $"{actionName} | Entity with given id ( {entityId} ) not found in {dbSetName} DbSet.";
		}

		public const string ExceptionOccuredLogger = "Error {exceptionType} occured on method: {methodName}";
		public const string ConflictBetweenTodoListIdsAsParamAndFromModelObjectLogger = "{operationName} | Conflict occured! Given id ( {todoListId} ) for To Do List and To Do List id in Model object ( {taskTodoListId} ) are not equal!";
		public const string ConflictBetweenTodoListIdsFromTodoListModelAndTaskModelLogger = "{operationName} | Conflict occured! To Do List id ( {taskModelTodoListId} ) from Task object and target To Do List id ( {todoListId} ) in To Do List object are not equal!";
		public const string ErrorOnMethodLogger = "Error errors occured on method: {methodName}";
		public const string TransactionFailedLogger = "{operationName} | Transaction failed! - entity id {idValue}";
		public const string EntityNotFoundInDbLogger = "{operationName} | Context returned null for object {objectName}.";
		public const string EntityNotFoundInDbSetLogger = "{actionName} | Entity with given id ( {entityId} ) not found in {dbSetName} DbSet.";
		public const string OutOfRangeLogger = "{actionName} | Given param {paramName} with value ( {paramValue} ) is out of range!";
		public const string BuildingSuccedLogger = "{typeName} - {actionName} succeed!";
		public const string ParamObjectNullLogger = "{actionName} | Provided param object {objectName} cannot be null!";
		public const string DbSetNullLogger = "{actionName} | DbSet - {dbSetName} cannot be null!";
		public const string ProvidedObjectNullEx = "Critical error! Provided object cannot be null!";
		public const string DbSetNullEx = "Critical error! Dbset object cannot be null!";
		public const string ItemNotFoundInDb = "There's not such item in Database with given id.";
		public const string NotAnyTodoListInDb = "{operationName} | There's not any To Do List in Database.";
		public const string CreatingUserIdentityFailed = "{operationName} | Creating new User Identity failed!";
		public const string InvalidLoginData = "Invalid name or password.";
		public const string NameTaken = "Name already taken! Try again and get new one!";
	}
}
