using App.Features.Exceptions.Throw;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Features.Tasks.Show;

public class ShowTaskHandler : IRequestHandler<ShowTaskQuery, ShowTaskQueryResponse>
{
	private readonly ITaskRepository _taskRepository;
	private readonly ILogger<ShowTaskHandler> _logger;
	private readonly ITaskEntityMapper _taskEntityMapper;
	private readonly ITaskViewModelsFactory _taskViewModelsFactory;

	public ShowTaskHandler(ITaskRepository taskRepository, ILogger<ShowTaskHandler> logger, 
		ITaskEntityMapper taskEntityMapper, ITaskViewModelsFactory taskViewModelsFactory)
	{
		_taskRepository = taskRepository;
		_logger = logger;
		_taskEntityMapper = taskEntityMapper;
		_taskViewModelsFactory = taskViewModelsFactory;
	}

	public async Task<ShowTaskQueryResponse> Handle(ShowTaskQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(ShowTaskQuery), request.TodoListId, nameof(request.TodoListId), _logger);
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(ShowTaskQuery), request.TaskId, nameof(request.TaskId), _logger);

		TaskModel? taskModel = await _taskRepository
			.GetEntity(request.TaskId)
			.SingleOrDefaultAsync();

		ExceptionsService.WhenEntityIsNullThrowCritical(nameof(ShowTaskQuery), taskModel, _logger, request.TaskId);
		ExceptionsService.WhenIdsAreNotEqualThrowCritical(nameof(ShowTaskQuery), request.TodoListId, nameof(request.TodoListId), taskModel!.TodoListId, nameof(taskModel.TodoListId), _logger);

		TaskDto taskDto = _taskEntityMapper.TransferToDto(taskModel);
		var data = _taskViewModelsFactory.CreateDetailsOutputVM(taskDto);

		return new ShowTaskQueryResponse(data);
	}
}
