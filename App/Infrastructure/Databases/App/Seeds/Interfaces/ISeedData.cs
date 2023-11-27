using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Common.Models;

namespace App.Infrastructure.Databases.App.Seeds.Interfaces;

/// <summary>
/// Class provides sets with data to seed main Db.
/// </summary>
public interface ISeedData
{
	public ICollection<TaskModel> AllTasks { get; }

	/// <summary>
	/// All Todolists for Database Set.
	/// </summary>
	public ICollection<TodoListModel> TodoLists { get; }

	/// <summary>
	/// Specific 'UX' Tasks for a list.
	/// </summary>
	public ICollection<TaskModel> TasksUX { get;}

	/// <summary>
	/// Specific 'Backend' Tasks for a list.
	/// </summary>
	public ICollection<TaskModel> TasksBackend { get; }

	/// <summary>
	/// Specific 'Testing' Tasks for a list.
	/// 
	/// </summary>
	public ICollection<TaskModel> TasksTesting { get; }

	/// <summary>
	/// Specific 'Project Management' Tasks for a list.
	/// 
	/// </summary>
	public ICollection<TaskModel> TasksProjectManagement { get; }
}
