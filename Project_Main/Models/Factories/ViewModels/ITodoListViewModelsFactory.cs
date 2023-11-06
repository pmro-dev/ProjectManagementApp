using Project_Main.Models.DTOs;
using Project_Main.Models.Generics.ViewModels.WrapperModels;
using Project_Main.Models.Inputs.ViewModels;
using Project_Main.Models.Outputs.ViewModels;

namespace Project_Main.Models.Factories.ViewModels
{
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
}
