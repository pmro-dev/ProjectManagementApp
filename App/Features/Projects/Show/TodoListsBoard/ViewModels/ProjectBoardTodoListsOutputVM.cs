namespace App.Features.Projects.Show.TodoListsBoard.ViewModels;

public class ProjectBoardTodoListsOutputVM : IProjectBoardTodoListsOutputVM
{
    public ICollection<ITodoListsBoardTodoListVM> BrieflyTodoLists { get; set; } = new List<ITodoListsBoardTodoListVM>();
}
