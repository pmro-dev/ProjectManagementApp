using Web.Common.ViewModels;
using Web.TodoLists.Create;
using Web.TodoLists.Delete;
using Web.TodoLists.Edit;
using Web.TodoLists.Show;

namespace Web.TodoLists.Common.Interfaces;

public interface ITodoListViewModelsFactory
{
    TodoListCreateOutputVM CreateCreateOutputVM(string userId);
    TodoListCreateInputVM CreateCreateInputVM(ITodoListDto todoListDto);
    TodoListEditOutputVM CreateEditOutputVM(ITodoListDto todoListDto);
    TodoListEditInputVM CreateEditInputVM(ITodoListDto todoListDto);
    TodoListDeleteOutputVM CreateDeleteOutputVM(ITodoListDto todoListDto);
    TodoListDetailsOutputVM CreateDetailsOutputVM(ITodoListDto todoListDto, DateTime? filterDueDate);
    WrapperViewModel<TodoListCreateInputVM, TodoListCreateOutputVM> CreateWrapperCreateVM();
    WrapperViewModel<TodoListEditInputVM, TodoListEditOutputVM> CreateWrapperEditVM();
}
