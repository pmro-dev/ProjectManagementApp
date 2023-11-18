using App.Common.ViewModels;
using App.Features.Tasks.Common;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Features.TodoLists.Create;
using App.Features.TodoLists.Delete;
using App.Features.TodoLists.Edit;
using App.Features.TodoLists.Show;

namespace App.Features.TodoLists.Common;

public class TodoListViewModelsFactory : ITodoListViewModelsFactory
{
	public TodoListCreateInputVM CreateCreateInputVM(TodoListDto todoListDto)
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

	public TodoListDeleteOutputVM CreateDeleteOutputVM(TodoListDto todoListDto)
	{
		return new TodoListDeleteOutputVM()
		{
			Id = todoListDto.Id,
			Title = todoListDto.Title,
			TasksCount = todoListDto.Tasks.Count
		};
	}

	public TodoListDetailsOutputVM CreateDetailsOutputVM(TodoListDto todoListDto, DateTime? filterDueDate)
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

	public TodoListEditInputVM CreateEditInputVM(TodoListDto todoListDto)
	{
		return new TodoListEditInputVM
		{
			Title = todoListDto.Title
		};
	}

	public TodoListEditOutputVM CreateEditOutputVM(TodoListDto todoListDto)
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
