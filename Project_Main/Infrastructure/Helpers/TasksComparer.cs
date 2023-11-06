using Project_Main.Models.DTOs;

namespace Project_Main.Infrastructure.Helpers
{
    /// <summary>
    /// Class allows to compare object based on the same Task Model type.
    /// </summary>
    public class TasksComparer : IComparer<ITaskDto>
	{
		public int Compare(ITaskDto? x, ITaskDto? y)
		{
			if (x is null && y is null)
			{
				return 0;
			}

			if (x is null)
			{
				return -1;
			}

			if (y is null)
			{
				return 1;
			}

			return x.DueDate.CompareTo(y.DueDate);
		}
	}
}
