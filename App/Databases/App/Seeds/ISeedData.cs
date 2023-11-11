using Web.Tasks.Common.Interfaces;
using Web.TodoLists.Common.Interfaces;

namespace Web.Databases.App.Seeds;

/// <summary>
/// Class provides sets with data to seed main Db.
/// </summary>
public interface ISeedData
{
	public ICollection<ITaskModel> AllTasks { get; set; }

	/// <summary>
	/// All Todolists for Database Set.
	/// </summary>
	public ICollection<ITodoListModel> TodoLists { get; set; }

	/// <summary>
	/// Specific 'UX' Tasks for a list.
	/// </summary>
	public ICollection<ITaskModel> TasksUX { get; set; }

	/// <summary>
	/// Specific 'Backend' Tasks for a list.
	/// </summary>
	public ICollection<ITaskModel> TasksBackend { get; set; }

	/// <summary>
	/// Specific 'Testing' Tasks for a list.
	/// 
	/// </summary>
	public ICollection<ITaskModel> TasksTesting { get; set; }

	/// <summary>
	/// Specific 'Project Management' Tasks for a list.
	/// 
	/// </summary>
	public ICollection<ITaskModel> TasksProjectManagement { get; set; }
}
