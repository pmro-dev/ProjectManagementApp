using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Main.Infrastructure.DTOs.Entities;
using Project_Main.Models.ViewModels.InputModels;
using Project_Main.Models.ViewModels.OutputModels;
using Project_Main.Models.ViewModels.WrapperModels;

namespace Project_Main.Services.DTO.ViewModelsFactories
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
