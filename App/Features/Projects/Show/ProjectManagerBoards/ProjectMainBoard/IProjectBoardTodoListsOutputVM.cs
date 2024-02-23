using App.Features.Projects.Show.ProjectManagerBoards.TodoListsBoard;

namespace App.Features.Projects.Show.ProjectManagerBoards.ProjectBoard;

public interface IProjectBoardTodoListsOutputVM
{
    ICollection<ITodoListsBoardTodoListVM> BrieflyTodoLists { get; set; }
}
