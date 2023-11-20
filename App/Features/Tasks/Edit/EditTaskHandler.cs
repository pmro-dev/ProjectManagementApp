using App.Common.Helpers;
using App.Common.ViewModels;
using App.Features.Tasks.Common;
using App.Features.TodoLists.Common.Models;
using App.Infrastructure.Helpers;
using MediatR;
using App.Features.Tasks.Common.Interfaces;
using App.Features.TodoLists.Common.Interfaces;
using App.Infrastructure.Databases.App.Interfaces;

namespace App.Features.Tasks.Edit;

public class EditTaskHandler : IRequestHandler<EditTaskQuery, WrapperViewModel<TaskEditInputVM, TaskEditOutputVM>>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITaskRepository _taskRepository;
	private readonly ITodoListRepository _todoListRepository;
	private readonly ILogger<TaskController> _logger;
	private readonly ITaskEntityMapper _taskEntityMapper;
	private readonly ITodoListMapper _todoListMapper;
	private readonly ITaskViewModelsFactory _taskViewModelsFactory;

	public EditTaskHandler(IDataUnitOfWork dataUnitOfWork, ITaskRepository taskRepository, ITodoListRepository todoListRepository, 
		ILogger<TaskController> logger, ITaskEntityMapper taskEntityMapper, ITodoListMapper todoListMapper, 
		ITaskViewModelsFactory taskViewModelsFactory)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_taskRepository = taskRepository;
		_todoListRepository = todoListRepository;
		_logger = logger;
		_taskEntityMapper = taskEntityMapper;
		_todoListMapper = todoListMapper;
		_taskViewModelsFactory = taskViewModelsFactory;
	}

	public async Task<WrapperViewModel<TaskEditInputVM, TaskEditOutputVM>> Handle(EditTaskQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Edit), request.TodoListId, nameof(request.TodoListId), _logger);
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Edit), request.TaskId, nameof(request.TaskId), _logger);

		TaskModel? taskModel = await _taskRepository.GetAsync(request.TaskId);

		if (taskModel is null)
		{
			//TODO
			_logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, nameof(Edit), nameof(TaskModel), request.TaskId);
			//return NotFound();
		}

		TodoListModel? targetTodoListModel = await _todoListRepository.GetAsync(request.TodoListId);

		if (targetTodoListModel is null)
		{
			//TODO
			_logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, nameof(Edit), nameof(TodoListModel), request.TodoListId);
			//return NotFound();
		}

		ExceptionsService.WhenIdsAreNotEqualThrowCritical(nameof(Edit), taskModel!.TodoListId, nameof(taskModel.TodoListId), targetTodoListModel!.Id, nameof(targetTodoListModel.Id), _logger);

		TaskDto taskDto = _taskEntityMapper.TransferToDto(taskModel);

		// TODO implement method that allow to get only concrete properties by Select expression, here I need only TodoLists Ids and Names
		ICollection<TodoListModel>? userTodoListModels = await _todoListRepository.GetAllByFilterAsync(todoList => todoList.UserId == request.SignedInUserId);

		ExceptionsService.ThrowGroupOfEntitiesNotFoundInDb(nameof(Edit), nameof(ICollection<TodoListModel>), _logger);

		ICollection<TodoListDto> userTodoListDtos = _todoListMapper.TransferToDto(userTodoListModels);
		// END OF TO DO

		var editOutputVM = _taskViewModelsFactory.CreateEditOutputVM(taskDto, userTodoListDtos);
		var editWrapperVM = _taskViewModelsFactory.CreateWrapperEditVM();
		editWrapperVM.OutputVM = editOutputVM;

		return editWrapperVM;
	}
}
