namespace Project_Main.Infrastructure.Helpers
{
	/// <summary>
	/// Helper to check for exceptions to throw.
	/// </summary>
	public static class HelperCheck
	{
		/// <summary>
		/// Throws and Logs exception when model object is null.
		/// </summary>
		/// <typeparam name="T">Model data type.</typeparam>
		/// <param name="operationName">Name of operation during exception could occured.</param>
		/// <param name="model">Object of model data.</param>
		/// <param name="modelName">Model name.</param>
		/// <param name="logger">Logger from class that invokes method.</param>
		/// <exception cref="ArgumentNullException">Occurs when model is null.</exception>
		public static void IfModelNullThrowException<T>(string operationName, T model, string modelName, ILogger logger) where T : class
		{
			if (model == null)
			{
				logger.LogError(Messages.LogParamNullOrEmpty, operationName, modelName);
				throw new ArgumentNullException(modelName, Messages.ProvidedObjectIsNull);
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
		public static void IfArgumentIsNullOrEmptyThrowException(string operationName, object argument, string argumentName, ILogger logger)
		{
			if (argument is null)
			{
				logger.LogError(Messages.LogParamNullOrEmpty, operationName, argumentName);
				throw new ArgumentNullException(argumentName, Messages.ProvidedObjectIsNull);
			}

			if (argument is string idString)
			{
				if (string.IsNullOrEmpty(idString))
				{
					logger.LogError(Messages.LogParamNullOrEmpty, operationName, argumentName);
					throw new ArgumentNullException(argumentName, Messages.ProvidedArgumentIsNullOrEmpty);
				}
			}
			else if (argument is int idInt)
			{
				if (idInt < 0)
				{
					logger.LogError(Messages.LogOutOfRange, operationName, argumentName, argument);
					throw new ArgumentOutOfRangeException(argumentName, Messages.ProvidedArgumentIsOutOfRange);
				}
			}
			else
			{
				logger.LogError(Messages.LogInvalidArgumentType, operationName, argumentName);
				throw new ArgumentException(Messages.ProvidedArgumentIsWithInvalidType, argumentName);
			}
		}

		/// <summary>
		/// Throws and Logs exception when string param is null or empty.
		/// </summary>
		/// <param name="operationName">Name of operation during exception could occured.</param>
		/// <param name="logger">Logger from class that invokes method.</param>
		/// <param name="param">Some string param.</param>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when User Id is null.</exception>
		public static void IFParamNullOrEmptyThrowException(string operationName, ref string param, string paramName, ILogger logger)
		{
			if (string.IsNullOrEmpty(param))
			{
				logger.LogError(Messages.LogParamNullOrEmpty, operationName, paramName);
				throw new ArgumentOutOfRangeException(Messages.ExceptionNullObjectOnAction(operationName, paramName));
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
		public static void CheckIdWhenLowerThanBottomBoundryThrowException(string operationName, int id, string paramName, int bottomBoundry, ILogger logger)
		{
			if (id < bottomBoundry)
			{
				logger.LogInformation(Messages.LogOutOfRange, operationName, paramName, id);
				throw new ArgumentOutOfRangeException(paramName, id, Messages.OutOfRange(operationName, nameof(id), id));
			}
		}
	}
}
