using App.Common.Helpers;
using App.Common.ViewModels;
using App.Features.Tasks.Common;
using App.Features.Tasks.Common.Interfaces;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Helpers;
using MediatR;

namespace App.Features.Tasks.Create;

public class CreateTaskHandler : IRequestHandler<CreateTaskQuery, WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM>>, IRequestHandler<CreateTaskCommand, object>
{
	private readonly ILogger<CreateTaskHandler> _logger;
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITodoListRepository _todoListRepository;
	private readonly ITaskRepository _taskRepository;
	private readonly ITodoListMapper _todoListMapper;
	private readonly ITaskEntityMapper _taskEntityMapper;
	private readonly ITaskViewModelsFactory _taskViewModelsFactory;

	public CreateTaskHandler(ILogger<CreateTaskHandler> logger, ITodoListRepository todoListRepository, ITodoListMapper todoListMapper, 
		ITaskViewModelsFactory taskViewModelsFactory, IDataUnitOfWork dataUnitOfWork, ITaskRepository taskRepository, 
		ITaskEntityMapper taskEntityMapper)
	{
		_logger = logger;
		_todoListRepository = todoListRepository;
		_todoListMapper = todoListMapper;
		_taskViewModelsFactory = taskViewModelsFactory;
		_dataUnitOfWork = dataUnitOfWork;
		_taskRepository = taskRepository;
		_taskEntityMapper = taskEntityMapper;
	}

	public async Task<WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM>> Handle(CreateTaskQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Create), request.TaskId, nameof(request.TaskId), _logger);

		TodoListModel? todoListModel = await _todoListRepository.GetAsync(request.TaskId);

		if (todoListModel is null)
		{
			//TODO implement failure for result
			_logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, "Constructing" + nameof(CreateTaskHandler), nameof(TodoListModel), request.TaskId);
			throw new InvalidOperationException("Task creation for todolist failed because there is no such Todolist in Database!");
			//return NotFound();
		}

		TodoListDto todoListDto = _todoListMapper.TransferToDto(todoListModel);
		var taskCreateOutputVM = _taskViewModelsFactory.CreateCreateOutputVM(todoListDto);
		var taskCreateWrapperVM = _taskViewModelsFactory.CreateWrapperCreateVM();
		taskCreateWrapperVM.OutputVM = taskCreateOutputVM;

		return taskCreateWrapperVM;
	}

	public async Task<object> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
	{
		TaskCreateInputVM inputVM = request.TaskCreateInputVM;
		TaskDto taskDto = _taskEntityMapper.TransferToDto(inputVM);
		TaskModel taskModel = _taskEntityMapper.TransferToModel(taskDto);

		await _taskRepository.AddAsync(taskModel);
		await _dataUnitOfWork.SaveChangesAsync();

		object routeValue = new { id = taskDto.TodoListId };

		return routeValue;
	}
}
