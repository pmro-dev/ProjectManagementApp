using Web.Common.Helpers;

namespace Web.Infrastructure.Helpers
{
	/// <summary>
	/// Helper to check for exceptions to throw.
	/// </summary>
	public static class HelperCheck
	{
		public const int IdBottomBoundry = 0;

		/// <summary>
		/// Throws and Logs exception when model object is null.
		/// </summary>
		/// <typeparam name="T">Model data type.</typeparam>
		/// <param name="operationName">Name of operation during exception could occured.</param>
		/// <param name="model">Object of model data.</param>
		/// <param name="modelName">Model name.</param>
		/// <param name="logger">Logger from class that invokes method.</param>
		/// <exception cref="ArgumentNullException">Occurs when model is null.</exception>
		public static void ThrowExceptionWhenModelNull<T>(string operationName, T model, string modelName, ILogger logger) where T : class
		{
			if (model == null)
			{
				logger.LogError(MessagesPacket.LogParamNullOrEmpty, operationName, modelName);
				throw new ArgumentNullException(modelName, MessagesPacket.ProvidedObjectIsNull);
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
		public static void ThrowExceptionWhenArgumentIsNullOrEmpty(string operationName, object argument, string argumentName, ILogger logger)
		{
			if (argument is null)
			{
				logger.LogError(MessagesPacket.LogParamNullOrEmpty, operationName, argumentName);
				throw new ArgumentNullException(argumentName, MessagesPacket.ProvidedObjectIsNull);
			}

			if (argument is string idString)
			{
				if (string.IsNullOrEmpty(idString))
				{
					logger.LogError(MessagesPacket.LogParamNullOrEmpty, operationName, argumentName);
					throw new ArgumentNullException(argumentName, MessagesPacket.ProvidedArgumentIsNullOrEmpty);
				}
			}
			else if (argument is int idInt)
			{
				if (idInt < 0)
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
		/// <param name="param">Some string param.</param>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when User Id is null.</exception>
		public static void ThrowExceptionWhenParamNullOrEmpty(string operationName, ref string param, string paramName, ILogger logger)
		{
			if (string.IsNullOrEmpty(param))
			{
				logger.LogError(MessagesPacket.LogParamNullOrEmpty, operationName, paramName);
				throw new ArgumentNullException(MessagesPacket.ExceptionNullObjectOnAction(operationName, paramName));
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
		public static void ThrowExceptionWhenIdLowerThanBottomBoundry(string operationName, int id, string paramName, int bottomBoundry, ILogger logger)
		{
			if (id < bottomBoundry)
			{
				logger.LogError(MessagesPacket.LogOutOfRange, operationName, paramName, id);
				throw new ArgumentOutOfRangeException(paramName, id, MessagesPacket.OutOfRange(operationName, nameof(id), id));
			}
		}
	}
}
