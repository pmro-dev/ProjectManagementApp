using Project_DomainEntities;

namespace ClassLibrary_SeedData
{
	public interface ISeedData
	{
		public List<TaskModel> AllTasks { get; set; }

		/// <summary>
		/// All Todolists for Database Set.
		/// </summary>
		public List<TodoListModel> TodoLists { get; set; }

		/// <summary>
		/// Specific 'UX' Tasks for a list.
		/// </summary>
		public List<TaskModel> TasksUX { get; set; }

		/// <summary>
		/// Specific 'Backend' Tasks for a list.
		/// </summary>
		public List<TaskModel> TasksBackend { get; set; }

		/// <summary>
		/// Specific 'Testing' Tasks for a list.
		/// 
		/// </summary>
		public List<TaskModel> TasksTesting { get; set; }

		/// <summary>
		/// Specific 'Project Management' Tasks for a list.
		/// 
		/// </summary>
		public List<TaskModel> TasksProjectManagement { get; set; }
	}
}
