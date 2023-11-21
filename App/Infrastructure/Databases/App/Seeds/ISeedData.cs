using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Common.Models;

namespace App.Infrastructure.Databases.App.Seeds;

/// <summary>
/// Class provides sets with data to seed main Db.
/// </summary>
public interface ISeedData
{
	public ICollection<TaskModel> AllTasks { get; set; }

	/// <summary>
	/// All Todolists for Database Set.
	/// </summary>
	public ICollection<TodoListModel> TodoLists { get; set; }

	/// <summary>
	/// Specific 'UX' Tasks for a list.
	/// </summary>
	public ICollection<TaskModel> TasksUX { get; set; }

	/// <summary>
	/// Specific 'Backend' Tasks for a list.
	/// </summary>
	public ICollection<TaskModel> TasksBackend { get; set; }

	/// <summary>
	/// Specific 'Testing' Tasks for a list.
	/// 
	/// </summary>
	public ICollection<TaskModel> TasksTesting { get; set; }

	/// <summary>
	/// Specific 'Project Management' Tasks for a list.
	/// 
	/// </summary>
	public ICollection<TaskModel> TasksProjectManagement { get; set; }
}
