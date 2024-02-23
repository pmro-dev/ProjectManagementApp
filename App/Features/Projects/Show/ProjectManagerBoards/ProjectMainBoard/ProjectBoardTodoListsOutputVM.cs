using App.Features.Projects.Show.ProjectManagerBoards.TodoListsBoard;

namespace App.Features.Projects.Show.ProjectManagerBoards.ProjectBoard;

public class ProjectBoardTodoListsOutputVM : IProjectBoardTodoListsOutputVM
{
    public ICollection<ITodoListsBoardTodoListVM> BrieflyTodoLists { get; set; } = new List<ITodoListsBoardTodoListVM>();
}
