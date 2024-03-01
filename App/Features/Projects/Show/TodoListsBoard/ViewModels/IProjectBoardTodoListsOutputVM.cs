namespace App.Features.Projects.Show.TodoListsBoard.ViewModels;

public interface IProjectBoardTodoListsOutputVM
{
    ICollection<ITodoListsBoardTodoListVM> BrieflyTodoLists { get; set; }
}
