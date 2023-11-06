using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Main.Models.DTOs;
using Project_Main.Models.Generics.ViewModels.WrapperModels;
using Project_Main.Models.Inputs.ViewModels;
using Project_Main.Models.Outputs.ViewModels;

namespace Project_Main.Models.Factories.ViewModels
{
    public interface ITaskViewModelsFactory
    {
        public TaskDetailsOutputVM CreateDetailsOutputVM(ITaskDto taskDto);
        public TaskCreateOutputVM CreateCreateOutputVM(ITodoListDto todoListDto);
        public TaskCreateInputVM CreateCreateInputVM(ITaskDto taskDto);
        public WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM> CreateWrapperCreateVM();
        public WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM> CreateWrapperCreateVM(ITaskDto taskDto, ITodoListDto todoListDto);
        public WrapperViewModel<TaskEditInputVM, TaskEditOutputVM> CreateWrapperEditVM();
        public WrapperViewModel<TaskDeleteInputVM, TaskDeleteOutputVM> CreateWrapperDeleteVM();
        public TaskEditInputVM CreateEditInputVM(ITaskDto taskDto, SelectList statusSelector, SelectList todoListSelector);
        public TaskEditOutputVM CreateEditOutputVM(ITaskDto taskDto, SelectList statusSelector, SelectList todoListSelector);
        public TaskDeleteOutputVM CreateDeleteOutputVM(ITaskDto taskDto);
        public TaskDeleteInputVM CreateDeleteInputVM(int id, int todoListId);
    }
}
