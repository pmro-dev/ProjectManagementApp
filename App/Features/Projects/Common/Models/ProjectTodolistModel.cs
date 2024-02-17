using App.Features.Projects.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Projects.Common.Models;

public class ProjectTodolistModel : IProjectTodolistModel
{
	[Timestamp]
	public byte[] RowVersion { get; set; }

	[Required]
	public Guid TodoListId { get; set; }

	[ForeignKey(nameof(TodoListId))]
	public TodoListModel? TodoList { get; set; }

	[Required]
	public Guid ProjectId { get; set; }

	[ForeignKey(nameof(ProjectId))]
	public ProjectModel? Project { get; set; }

	public ProjectTodolistModel(Guid todoListId, Guid projectId)
	{
		TodoListId = todoListId;
		ProjectId = projectId;
		RowVersion = new byte[] { 1, 1, 1 };
	}
}
