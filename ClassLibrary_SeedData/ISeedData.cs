using Project_DomainEntities;

namespace ClassLibrary_SeedData
{
	/// <summary>
	/// Class provides sets with data to seed main Db.
	/// </summary>
	public interface ISeedData
	{
		public List<ITaskModel> AllTasks { get; set; }

		/// <summary>
		/// All Todolists for Database Set.
		/// </summary>
		public List<ITodoListModel> TodoLists { get; set; }

		/// <summary>
		/// Specific 'UX' Tasks for a list.
		/// </summary>
		public List<ITaskModel> TasksUX { get; set; }

		/// <summary>
		/// Specific 'Backend' Tasks for a list.
		/// </summary>
		public List<ITaskModel> TasksBackend { get; set; }

		/// <summary>
		/// Specific 'Testing' Tasks for a list.
		/// 
		/// </summary>
		public List<ITaskModel> TasksTesting { get; set; }

		/// <summary>
		/// Specific 'Project Management' Tasks for a list.
		/// 
		/// </summary>
		public List<ITaskModel> TasksProjectManagement { get; set; }
	}
}
