using App.Common.Helpers;
using App.Features.Tasks.Common;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Show.Interfaces;
using App.Infrastructure.Databases.App.Interfaces;
using App.Infrastructure.Helpers;
using MediatR;

namespace App.Features.Tasks.Show;

public class ShowTaskHandler : IRequestHandler<ShowTaskQuery, IShowTaskOutputVM>
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

	public async Task<IShowTaskOutputVM> Handle(ShowTaskQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(ShowTaskQuery), request.TodoListId, nameof(request.TodoListId), _logger);
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(ShowTaskQuery), request.TaskId, nameof(request.TaskId), _logger);

		TaskModel? taskModel = await _taskRepository.GetAsync(request.TaskId);

		if (taskModel is null)
		{
			_logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, nameof(ShowTaskQuery), nameof(TaskModel), request.TaskId);
			
			//TODO implement failure result
			//return NotFound();
		}

		ExceptionsService.WhenIdsAreNotEqualThrowCritical(nameof(ShowTaskQuery), request.TodoListId, nameof(request.TodoListId), taskModel.TodoListId, nameof(taskModel.TodoListId), _logger);

		TaskDto taskDto = _taskEntityMapper.TransferToDto(taskModel);
		var resultAsDetailsOutputVM = _taskViewModelsFactory.CreateDetailsOutputVM(taskDto);

		return resultAsDetailsOutputVM;
	}
}
