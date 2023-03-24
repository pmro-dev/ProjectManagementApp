using TODO_Domain_Entities;

namespace TODO_List_ASPNET_MVC.Infrastructure.Helpers
{
	public class TasksComparer : IComparer<TaskModel>
	{
		public int Compare(TaskModel? x, TaskModel? y)
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
