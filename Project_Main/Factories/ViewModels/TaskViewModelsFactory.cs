using Application.DTOs.Entities;
using Application.Services;
using Web.ViewModels.Inputs;
using Web.ViewModels.Outputs;
using Web.ViewModels.Wrappers;

namespace Web.Factories.ViewModels;

public class TaskViewModelsFactory : ITaskViewModelsFactory
{
	private readonly IServiceProvider _serviceProvider;

	public TaskViewModelsFactory(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public TaskCreateInputVM CreateCreateInputVM(ITaskDto taskDto)
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

	public TaskCreateOutputVM CreateCreateOutputVM(ITodoListDto todoListDto)
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

	public TaskDeleteOutputVM CreateDeleteOutputVM(ITaskDto taskDto)
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

	public TaskDetailsOutputVM CreateDetailsOutputVM(ITaskDto taskDto)
	{
		return new TaskDetailsOutputVM()
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

	public TaskEditInputVM CreateEditInputVM(ITaskDto taskDto)
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

	private ISelectListService GetSelectListService()
	{
		return _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ISelectListService>();
	}

	public TaskEditOutputVM CreateEditOutputVM(ITaskDto taskDto, ICollection<ITodoListDto> userTodoListDtos)
	{
		var selectListService = GetSelectListService();
		var todoListSelectorDto = selectListService.CreateTodoListSelector(userTodoListDtos, taskDto.TodoListId);
		var taskStatusSelectorDto = selectListService.CreateTaskStatusSelector(taskDto);

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

	public WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM> CreateWrapperCreateVM(ITaskDto taskDto, ITodoListDto todoListDto)
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
