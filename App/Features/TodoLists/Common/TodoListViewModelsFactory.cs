using App.Common.ViewModels;
using App.Features.Pagination;
using App.Features.Tasks.Common;
using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Features.TodoLists.Create.Models;
using App.Features.TodoLists.Delete.Models;
using App.Features.TodoLists.Edit.Models;
using App.Features.TodoLists.Show.Models;

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

	public TodoListDetailsOutputVM CreateDetailsOutputVM(TodoListDto todoListDto, PaginationData paginationData, DateTime? filterDueDate)
	{
		var tasksForTodayDtos = Enumerable.Empty<TaskDto>();
		var tasksCompletedDtos = Enumerable.Empty<TaskDto>();
		var tasksNotCompletedDtos = Enumerable.Empty<TaskDto>();
		var tasksExpiredDtos = Enumerable.Empty<TaskDto>();

		//TODO create query with groupby by due date and task status type and get those groups
		Action[] filterTasks = {
					() => tasksForTodayDtos = TasksFilterService.FilterForTasksForToday(todoListDto.Tasks),
					() => tasksCompletedDtos = TasksFilterService.FilterForTasksCompleted(todoListDto.Tasks, filterDueDate),
					() => tasksNotCompletedDtos = TasksFilterService.FilterForTasksNotCompleted(todoListDto.Tasks, filterDueDate),
					() => tasksExpiredDtos = TasksFilterService.FilterForTasksExpired(todoListDto.Tasks, filterDueDate)};

		Parallel.Invoke(filterTasks);

		var detailsOutputVM = new TodoListDetailsOutputVM(
			todoListDto.Id,
			todoListDto.Title,
			todoListDto.UserId,
			paginationData,
			filterDueDate,
			tasksForTodayDtos.ToList(),
			tasksCompletedDtos.ToList(),
			tasksNotCompletedDtos.ToList(),
			tasksExpiredDtos.ToList());

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
