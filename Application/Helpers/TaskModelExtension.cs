using Domain.Interfaces.ForEntities;

namespace Application.Helpers;

public static class TaskModelExtension
{
	/// <summary>
	/// Compares properties of two Tasks and return result of that comparison.
	/// </summary>
	/// <param name="obj">Second Task compare to.</param>
	/// <returns>Result of compare -> true if certain properties (Title, Description) of objects are equal, otherwise false.</returns>
	public static bool Equals(this ITaskModel taskModel, object? obj)
	{
		if (obj == null || !taskModel.GetType().Equals(obj.GetType()))
		{
			return false;
		}
		else
		{
			var task = (ITaskModel)obj;

			if (taskModel.Title == task.Title && taskModel.Description == task.Description)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	/// <summary>
	/// Create and sets Hash Code.
	/// </summary>
	/// <returns></returns>
	public static int GetHashCode(this ITaskModel taskModel)
	{
		return HashCode.Combine(taskModel.Title, taskModel.Id);
	}
}
