using Web.Common.ViewModels;
using Web.Tasks.Create;
using Web.Tasks.Delete;
using Web.Tasks.Edit;
using Web.Tasks.Show;
using Web.TodoLists.Common.Interfaces;

namespace Web.Tasks.Common.Interfaces;

public interface ITaskViewModelsFactory
{
    public TaskDetailsOutputVM CreateDetailsOutputVM(ITaskDto taskDto);
    public TaskCreateOutputVM CreateCreateOutputVM(ITodoListDto todoListDto);
    public TaskCreateInputVM CreateCreateInputVM(ITaskDto taskDto);
    public WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM> CreateWrapperCreateVM();
    public WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM> CreateWrapperCreateVM(ITaskDto taskDto, ITodoListDto todoListDto);
    public WrapperViewModel<TaskEditInputVM, TaskEditOutputVM> CreateWrapperEditVM();
    public WrapperViewModel<TaskDeleteInputVM, TaskDeleteOutputVM> CreateWrapperDeleteVM();
    public TaskEditInputVM CreateEditInputVM(ITaskDto taskDto);
    public TaskEditOutputVM CreateEditOutputVM(ITaskDto taskDto, ICollection<ITodoListDto> userTodoListDtos);
    public TaskDeleteOutputVM CreateDeleteOutputVM(ITaskDto taskDto);
    public TaskDeleteInputVM CreateDeleteInputVM(int id, int todoListId);
}
