namespace App.Features.Projects.Show.Interfaces;

public interface IProjectBoardTodoListsOutputVM
{
	ICollection<ITodoListsBoardTodoListVM> BrieflyTodoLists { get; set; }
}
