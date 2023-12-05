using System.ComponentModel.DataAnnotations;

namespace Project_UnitTests.Helpers
{
	/// <summary>
	/// Data Annotation validator.
	/// </summary>
	public static class DataAnnotationValidator
	{
		/// <summary>
		/// Checks for data annotation validation fails and registers names of properties that violated rules.
		/// </summary>
		/// <typeparam name="T">Item type that have to be at least class type.</typeparam>
		/// <param name="item">Object of certain type (T) to valid.</param>
		/// <returns>List of properties that failed data annotation's validation.</returns>
		public static List<string> ValidNewObject<T>(T item) where T : class
		{
			List<string> propertiesNames = new();
			ValidationContext context = new(item, serviceProvider: null, items: null);
			List<ValidationResult> results = new();

			if (!Validator.TryValidateObject(item, context, results, true))
			{
				foreach (ValidationResult elem in results)
				{
					string propName = string.Empty;
					if (elem.MemberNames.Any())
					{
						propName = ((string[])elem.MemberNames)[0];
					}

					propertiesNames.Add(propName);
				}
			}

			return propertiesNames;
		}
	}
}
