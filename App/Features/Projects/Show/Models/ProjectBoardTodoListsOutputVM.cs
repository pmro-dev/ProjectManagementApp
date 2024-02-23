using App.Features.Projects.Show.Interfaces;

namespace App.Features.Projects.Show.Models;

public class ProjectBoardTodoListsOutputVM : IProjectBoardTodoListsOutputVM
{
	public ICollection<ITodoListsBoardTodoListVM> BrieflyTodoLists { get; set; } = new List<ITodoListsBoardTodoListVM>();
}
