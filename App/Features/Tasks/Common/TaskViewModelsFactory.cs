using App.Common.ViewModels;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Create;
using App.Features.Tasks.Delete;
using App.Features.Tasks.Edit;
using App.Features.Tasks.Show;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;

namespace App.Features.Tasks.Common;

public class TaskViewModelsFactory : ITaskViewModelsFactory
{
	private readonly IServiceProvider _serviceProvider;

	public TaskViewModelsFactory(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public TaskCreateInputVM CreateCreateInputVM(TaskDto taskDto)
	{
		return new TaskCreateInputVM()
		{
			Title = taskDto.Title,
			Description = taskDto.Description,
			DueDate = taskDto.DueDate,
			ReminderDate = taskDto.ReminderDate,
			TodoListId = taskDto.TodoListId,
			UserId = taskDto.UserId,
		};
	}

	public TaskCreateOutputVM CreateCreateOutputVM(TodoListDto todoListDto)
	{
		return new TaskCreateOutputVM
		{
			TodoListId = todoListDto.Id,
			UserId = todoListDto.UserId,
			TodoListName = todoListDto.Title
		};
	}

	public TaskDeleteInputVM CreateDeleteInputVM(int id, int todoListId)
	{
		return new TaskDeleteInputVM()
		{
			Id = id,
			TodoListId = todoListId
		};
	}

	public TaskDeleteOutputVM CreateDeleteOutputVM(TaskDto taskDto)
	{
		return new TaskDeleteOutputVM()
		{
			Id = taskDto.Id,
			Title = taskDto.Title,
			Description = taskDto.Description,
			CreationDate = taskDto.CreationDate,
			DueDate = taskDto.DueDate,
			LastModificationDate = taskDto.LastModificationDate,
			ReminderDate = taskDto.ReminderDate,
			Status = taskDto.Status,
			TodoListId = taskDto.TodoListId,
			UserId = taskDto.UserId
		};
	}

	public ShowTaskOutputVM CreateDetailsOutputVM(TaskDto taskDto)
	{
		return new ShowTaskOutputVM()
		{
			Id = taskDto.Id,
			Title = taskDto.Title,
			Description = taskDto.Description,
			CreationDate = taskDto.CreationDate,
			DueDate = taskDto.DueDate,
			ReminderDate = taskDto.ReminderDate,
			LastModificationDate = taskDto.LastModificationDate,
			Status = taskDto.Status,
			TodoListId = taskDto.TodoListId,
			UserId = taskDto.UserId
		};
	}

	public TaskEditInputVM CreateEditInputVM(TaskDto taskDto)
	{
		return new TaskEditInputVM()
		{
			Id = taskDto.Id,
			Description = taskDto.Description,
			DueDate = taskDto.DueDate,
			Status = taskDto.Status,
			ReminderDate = taskDto.ReminderDate,
			Title = taskDto.Title,
			TodoListId = taskDto.TodoListId,
			UserId = taskDto.UserId
		};
	}

	private (ITaskSelector, ITodoListSelector) GetSelectors()
	{
		return (_serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ITaskSelector>(), _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ITodoListSelector>());
	}

	public TaskEditOutputVM CreateEditOutputVM(TaskDto taskDto, ICollection<TodoListDto> userTodoListDtos)
	{
		var selectors = GetSelectors();
		var taskStatusSelectorDto = selectors.Item1.Create(taskDto);
		var todoListSelectorDto = selectors.Item2.Create(userTodoListDtos, taskDto.TodoListId);

		return new TaskEditOutputVM()
		{
			Id = taskDto.Id,
			Title = taskDto.Title,
			Description = taskDto.Description,
			DueDate = taskDto.DueDate,
			ReminderDate = taskDto.ReminderDate,
			Status = taskDto.Status,
			TodoListId = taskDto.TodoListId,
			UserId = taskDto.UserId,
			StatusSelector = taskStatusSelectorDto,
			TodoListSelector = todoListSelectorDto
		};
	}

	public WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM> CreateWrapperCreateVM(TaskDto taskDto, TodoListDto todoListDto)
	{
		TaskCreateInputVM inputVM = CreateCreateInputVM(taskDto);
		TaskCreateOutputVM outputVM = CreateCreateOutputVM(todoListDto);

		return new WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM>(inputVM, outputVM)
		{
			InputVM = inputVM,
			OutputVM = outputVM
		};
	}

	public WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM> CreateWrapperCreateVM()
	{
		return new WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM>();
	}

	public WrapperViewModel<TaskDeleteInputVM, TaskDeleteOutputVM> CreateWrapperDeleteVM()
	{
		return new WrapperViewModel<TaskDeleteInputVM, TaskDeleteOutputVM>();
	}

	public WrapperViewModel<TaskEditInputVM, TaskEditOutputVM> CreateWrapperEditVM()
	{
		return new WrapperViewModel<TaskEditInputVM, TaskEditOutputVM>();
	}
}
