using App.Common;
using App.Common.Helpers;
using App.Features.Exceptions.Throw;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Features.Tasks.Create;

public class CreateTaskHandler : 
	IRequestHandler<CreateTaskQuery, CreateTaskQueryResponse>, 
	IRequestHandler<CreateTaskCommand, CreateTaskCommandResponse>
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

	public async Task<CreateTaskQueryResponse> Handle(CreateTaskQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(CreateTaskQuery), request.TodoListId, nameof(request.TodoListId), _logger);

		TodoListModel? todoListModel = await _todoListRepository
			.GetEntity(request.TodoListId)
			.SingleOrDefaultAsync();

		ExceptionsService.WhenEntityIsNullThrowCritical(nameof(CreateTaskQuery), todoListModel, _logger, request.TodoListId);

		TodoListDto todoListDto = _todoListMapper.TransferToDto(todoListModel!);
		var taskCreateOutputVM = _taskViewModelsFactory.CreateCreateOutputVM(todoListDto);
		var taskCreateWrapperVM = _taskViewModelsFactory.CreateWrapperCreateVM();
		taskCreateWrapperVM.OutputVM = taskCreateOutputVM;

		return new CreateTaskQueryResponse(taskCreateWrapperVM);
	}

	public async Task<CreateTaskCommandResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
	{
		TaskDto taskDto = _taskEntityMapper.TransferToDto(request.InputVM);

        if (await _taskRepository.ContainsAnyAsync(task => task.Title == taskDto.Title && task.UserId == taskDto.UserId))
            return new CreateTaskCommandResponse(null, ExceptionsMessages.NameTaken, StatusCodesExtension.EntityNameTaken);

		TaskModel taskModel = _taskEntityMapper.TransferToModel(taskDto);

		await _taskRepository.AddAsync(taskModel);
		await _dataUnitOfWork.SaveChangesAsync();

		CustomRouteValues data = new () { Id = taskDto.TodoListId };

		return new CreateTaskCommandResponse(data);
	}
}
