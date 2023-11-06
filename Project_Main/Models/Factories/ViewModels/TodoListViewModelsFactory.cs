using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DTOs;
using Project_Main.Models.Generics.ViewModels.WrapperModels;
using Project_Main.Models.Inputs.ViewModels;
using Project_Main.Models.Outputs.ViewModels;
using Project_Main.Services;

namespace Project_Main.Models.Factories.ViewModels
{
    public class TodoListViewModelsFactory : ITodoListViewModelsFactory
    {
        public TodoListCreateInputVM CreateCreateInputVM(ITodoListDto todoListDto)
        {
            return new TodoListCreateInputVM()
            {
                UserId = todoListDto.UserId,
                Title = todoListDto.Title
            };
        }

        public TodoListCreateOutputVM CreateCreateOutputVM(string userId)
        {
            return new TodoListCreateOutputVM()
            {
                UserId = userId
            };
        }

        public TodoListDeleteOutputVM CreateDeleteOutputVM(ITodoListDto todoListDto)
        {
            return new TodoListDeleteOutputVM()
            {
                Id = todoListDto.Id,
                Title = todoListDto.Title,
                TasksCount = todoListDto.Tasks.Count()
            };
        }

        public TodoListDetailsOutputVM CreateDetailsOutputVM(ITodoListDto todoListDto, DateTime? filterDueDate)
        {
            var tasksForTodayDtos = TasksFilterService.FilterForTasksForToday(todoListDto.Tasks);
            var tasksCompletedDtos = TasksFilterService.FilterForTasksCompleted(todoListDto.Tasks);
            var tasksNotCompletedDtos = TasksFilterService.FilterForTasksNotCompleted(todoListDto.Tasks, filterDueDate);
            var tasksExpiredDtos = TasksFilterService.FilterForTasksExpired(todoListDto.Tasks);

            var detailsOutputVM = new TodoListDetailsOutputVM
            {
                Id = todoListDto.Id,
                Name = todoListDto.Title,
                TasksForToday = tasksForTodayDtos.ToList(),
                TasksCompleted = tasksCompletedDtos.ToList(),
                TasksNotCompleted = tasksNotCompletedDtos.ToList(),
                TasksExpired = tasksExpiredDtos.ToList()
            };

            var tasksComparer = new TasksComparer();

            var sortingTasksTasks = new Task[]
            {
                Task.Run(() => detailsOutputVM.TasksNotCompleted = detailsOutputVM.TasksNotCompleted.OrderBy(t => t, tasksComparer).ToList()),
                Task.Run(() => detailsOutputVM.TasksForToday = detailsOutputVM.TasksForToday.OrderBy(t => t, tasksComparer).ToList()),
                Task.Run(() => detailsOutputVM.TasksCompleted = detailsOutputVM.TasksCompleted.OrderBy(t => t, tasksComparer).ToList()),
                Task.Run(() => detailsOutputVM.TasksExpired = detailsOutputVM.TasksExpired.OrderBy(t => t, tasksComparer).ToList())
            };

            Task.WaitAll(sortingTasksTasks);

            return detailsOutputVM;
        }

        public TodoListEditInputVM CreateEditInputVM(ITodoListDto todoListDto)
        {
            return new TodoListEditInputVM
            {
                Title = todoListDto.Title
            };
        }

        public TodoListEditOutputVM CreateEditOutputVM(ITodoListDto todoListDto)
        {
            return new TodoListEditOutputVM
            {
                Id = todoListDto.Id,
                Title = todoListDto.Title,
                UserId = todoListDto.UserId
            };
        }

        public WrapperViewModel<TodoListCreateInputVM, TodoListCreateOutputVM> CreateWrapperCreateVM()
        {
            return new WrapperViewModel<TodoListCreateInputVM, TodoListCreateOutputVM>(new TodoListCreateInputVM(), new TodoListCreateOutputVM()) { };
        }

        public WrapperViewModel<TodoListEditInputVM, TodoListEditOutputVM> CreateWrapperEditVM()
        {
            return new WrapperViewModel<TodoListEditInputVM, TodoListEditOutputVM>(new TodoListEditInputVM(), new TodoListEditOutputVM()) { };
        }
    }
}
