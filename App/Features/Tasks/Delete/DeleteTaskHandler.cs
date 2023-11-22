#region USINGS

using App.Common.Helpers;
using App.Common.ViewModels;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Delete.Models;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

#endregion

namespace App.Features.Tasks.Delete;

public class DeleteTaskHandler : IRequestHandler<DeleteTaskQuery, WrapperViewModel<TaskDeleteInputVM, TaskDeleteOutputVM>>, IRequestHandler<DeleteTaskCommand, object>
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

	public async Task<WrapperViewModel<TaskDeleteInputVM, TaskDeleteOutputVM>> Handle(DeleteTaskQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Delete), request.TodoListId, nameof(request.TodoListId), _logger);
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Delete), request.TaskId, nameof(request.TaskId), _logger);

		TaskModel? taskToDeleteModel = await _taskRepository.GetAsync(request.TaskId);
		ExceptionsService.WhenModelIsNullThrowCritical(nameof(DeleteTaskQuery), taskToDeleteModel, _logger);

		TaskDto taskToDeleteDto = _taskEntityMapper.TransferToDto(taskToDeleteModel!);

		ExceptionsService.WhenIdsAreNotEqualThrowCritical(nameof(Delete), taskToDeleteDto.TodoListId, nameof(taskToDeleteDto.TodoListId), request.TodoListId, nameof(request.TodoListId), _logger);

		var deleteOutputVM = _taskViewModelsFactory.CreateDeleteOutputVM(taskToDeleteDto);
		var result = _taskViewModelsFactory.CreateWrapperDeleteVM();
		result.OutputVM = deleteOutputVM;

		return result;
	}

	public async Task<object> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
	{
		TaskDeleteInputVM inputVM = request.InputVM;

		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(DeleteTaskCommand), inputVM.TodoListId, nameof(inputVM.TodoListId), _logger);
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(DeleteTaskCommand), inputVM.Id, nameof(inputVM.Id), _logger);

		TaskDeleteInputDto taskInputDto = _taskEntityMapper.TransferToDto(inputVM);

		TaskModel? taskModel = await _taskRepository.GetAsync(taskInputDto.Id);
		ExceptionsService.WhenModelIsNullThrowCritical(nameof(DeleteTaskCommand), taskModel, _logger);

		ExceptionsService.WhenIdsAreNotEqualThrowCritical(nameof(DeleteTaskCommand), taskModel!.TodoListId, nameof(taskModel.TodoListId), taskInputDto.TodoListId, nameof(taskInputDto.TodoListId), _logger);

		_taskRepository.Remove(taskModel);
		await _dataUnitOfWork.SaveChangesAsync();

		object resultAsRouteValue = new { id = taskInputDto.TodoListId };

		return resultAsRouteValue;
	}
}
