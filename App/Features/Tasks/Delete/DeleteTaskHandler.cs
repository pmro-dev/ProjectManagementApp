#region USINGS

using App.Common;
using App.Common.ViewModels;
using App.Features.Exceptions.Throw;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Delete.Models;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

#endregion

namespace App.Features.Tasks.Delete;

public class DeleteTaskHandler : 
	IRequestHandler<DeleteTaskQuery, DeleteTaskQueryResponse>, 
	IRequestHandler<DeleteTaskCommand, DeleteTaskCommandResponse>
{
	private readonly ILogger<DeleteTaskHandler> _logger;
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITaskRepository _taskRepository;
	private readonly ITaskEntityMapper _taskEntityMapper;
	private readonly ITaskViewModelsFactory _taskViewModelsFactory;

	public DeleteTaskHandler(ILogger<DeleteTaskHandler> logger, IDataUnitOfWork dataUnitOfWork, ITaskRepository taskRepository, ITaskEntityMapper taskEntityMapper, ITaskViewModelsFactory taskViewModelsFactory)
	{
		_logger = logger;
		_dataUnitOfWork = dataUnitOfWork;
		_taskRepository = taskRepository;
		_taskEntityMapper = taskEntityMapper;
		_taskViewModelsFactory = taskViewModelsFactory;
	}

	public async Task<DeleteTaskQueryResponse> Handle(DeleteTaskQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Delete), request.TodoListId, nameof(request.TodoListId), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Delete), request.TaskId, nameof(request.TaskId), _logger);

		TaskModel? taskToDeleteModel = await _taskRepository.GetAsync(request.TaskId);
		ExceptionsService.WhenEntityIsNullThrow(nameof(DeleteTaskQuery), taskToDeleteModel, _logger, request.TaskId.ToString());

		TaskDto taskToDeleteDto = _taskEntityMapper.TransferToDto(taskToDeleteModel!);

		ExceptionsService.WhenIdsAreNotEqualThrow(
			nameof(Delete), 
			taskToDeleteDto.TodoListId, 
			nameof(taskToDeleteDto.TodoListId), 
			request.TodoListId, 
			nameof(request.TodoListId), 
			_logger);

		var deleteOutputVM = _taskViewModelsFactory.CreateDeleteOutputVM(taskToDeleteDto);
		var data = _taskViewModelsFactory.CreateWrapperDeleteVM();
		data.OutputVM = deleteOutputVM;

		return new DeleteTaskQueryResponse(data);
	}

	public async Task<DeleteTaskCommandResponse> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
	{
		TaskDeleteInputVM inputVM = request.InputVM;

		// TODO guid check
		//ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(DeleteTaskCommand), inputVM.TodoListId, nameof(inputVM.TodoListId), _logger);
		//ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(DeleteTaskCommand), inputVM.Id, nameof(inputVM.Id), _logger);

		TaskDeleteInputDto taskInputDto = _taskEntityMapper.TransferToDto(inputVM);

		TaskModel? taskModel = await _taskRepository.GetAsync(taskInputDto.Id);
		ExceptionsService.WhenEntityIsNullThrow(nameof(DeleteTaskCommand), taskModel, _logger, taskInputDto.Id);

		ExceptionsService.WhenIdsAreNotEqualThrow(nameof(DeleteTaskCommand), taskModel!.TodoListId, nameof(taskModel.TodoListId), taskInputDto.TodoListId, nameof(taskInputDto.TodoListId), _logger);

		_taskRepository.Remove(taskModel);
		await _dataUnitOfWork.SaveChangesAsync();

		CustomRouteValues data = new() { Id = taskInputDto.TodoListId };

		return new DeleteTaskCommandResponse(data);
	}
}
