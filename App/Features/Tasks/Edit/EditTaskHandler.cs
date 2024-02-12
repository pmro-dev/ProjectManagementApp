using App.Common.Helpers;
using App.Features.TodoLists.Common.Models;
using MediatR;
using App.Features.Tasks.Common.Interfaces;
using App.Features.TodoLists.Common.Interfaces;
using App.Infrastructure.Databases.App.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Edit.Models;
using App.Common;
using App.Features.Exceptions.Throw;

namespace App.Features.Tasks.Edit;

public class EditTaskHandler : 
	IRequestHandler<EditTaskQuery, EditTaskQueryResponse>, 
	IRequestHandler<EditTaskCommand, EditTaskCommandResponse>
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

	public async Task<EditTaskQueryResponse> Handle(EditTaskQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Edit), request.TodoListId, nameof(request.TodoListId), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Edit), request.TaskId, nameof(request.TaskId), _logger);

		TaskModel? taskModel = await _taskRepository.GetAsync(request.TaskId);
		ExceptionsService.WhenEntityIsNullThrow(nameof(EditTaskQuery), taskModel, _logger, request.TaskId);

		TodoListModel? targetTodoListModel = await _todoListRepository.GetAsync(request.TodoListId);
		ExceptionsService.WhenEntityIsNullThrow(nameof(EditTaskQuery), targetTodoListModel, _logger, request.TodoListId);

		ExceptionsService.WhenIdsAreNotEqualThrow(
			nameof(Edit), 
			taskModel!.TodoListId, 
			nameof(taskModel.TodoListId), 
			targetTodoListModel!.Id, 
			nameof(targetTodoListModel.Id), 
			_logger
		);

		TaskDto taskDto = _taskEntityMapper.TransferToDto(taskModel);

		// TODO implement method that allow to get only concrete properties by Select expression, here I need only TodoLists Ids and Names
		ICollection<TodoListModel> userTodoListModels = await _todoListRepository.GetAllByFilterAsync(todoList => todoList.OwnerId == request.SignedInUserId);
		ExceptionsService.WhenGroupOfRequiredEntitiesNotFoundInDbThrow(nameof(EditTaskQuery), userTodoListModels, _logger);

		ICollection<TodoListDto> userTodoListDtos = _todoListMapper.TransferToDto(userTodoListModels);
		// END OF TO DO

		var editOutputVM = _taskViewModelsFactory.CreateEditOutputVM(taskDto, userTodoListDtos);
		var editWrapperVM = _taskViewModelsFactory.CreateWrapperEditVM();
		editWrapperVM.OutputVM = editOutputVM;

		var data = editWrapperVM;
		return new EditTaskQueryResponse(data);
	}

	public async Task<EditTaskCommandResponse> Handle(EditTaskCommand request, CancellationToken cancellationToken)
	{
		TaskEditInputDto taskEditInputDto = _taskEntityMapper.TransferToDto(request.InputVM);

		TaskModel? taskDbModel = await _taskRepository.GetAsync(taskEditInputDto.Id);

		ExceptionsService.WhenEntityIsNullThrow(nameof(EditTaskCommand), taskDbModel, _logger, taskEditInputDto.Id);
		ExceptionsService.WhenIdsAreNotEqualThrow(
			nameof(EditTaskCommand), 
			taskDbModel!.Id, 
			nameof(taskDbModel.Id), 
			taskEditInputDto.Id, 
			nameof(taskEditInputDto.Id), 
			_logger
		);

		if (await _taskRepository
				.ContainsAny(task => 
					task.Title == taskEditInputDto.Title && 
					task.OwnerId == taskEditInputDto.OwnerId && 
					task.Title != taskEditInputDto.Title))
			return new EditTaskCommandResponse(null, ExceptionsMessages.NameTaken, StatusCodesExtension.EntityNameTaken);

		_taskEntityMapper.UpdateModel(taskDbModel, taskEditInputDto);
		_taskRepository.Update(taskDbModel);
		await _dataUnitOfWork.SaveChangesAsync();

		CustomRouteValues data = new() { Id = taskEditInputDto.TodoListId };

		return new EditTaskCommandResponse(data);
	}
}