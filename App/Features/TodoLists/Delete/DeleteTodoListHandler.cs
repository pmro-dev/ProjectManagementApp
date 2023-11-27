using App.Features.Exceptions.Throw;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.TodoLists.Delete;

public class DeleteTodoListHandler : 
	IRequestHandler<DeleteTodoListQuery, DeleteTodoListQueryResponse>, 
	IRequestHandler<DeleteTodoListCommand, DeleteTodoListCommandResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITodoListRepository _todoListRepository;
	private readonly ILogger<DeleteTodoListHandler> _logger;
	private readonly ITodoListMapper _todoListMapper;
	private readonly ITodoListViewModelsFactory _todoListViewModelsFactory;

	public DeleteTodoListHandler(IDataUnitOfWork dataUnitOfWork, ITodoListRepository todoListRepository, ILogger<DeleteTodoListHandler> logger,
		ITodoListMapper todoListMapper, ITodoListViewModelsFactory todoListViewModelsFactory)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_todoListRepository = todoListRepository;
		_logger = logger;
		_todoListMapper = todoListMapper;
		_todoListViewModelsFactory = todoListViewModelsFactory;
	}

	public async Task<DeleteTodoListQueryResponse> Handle(DeleteTodoListQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(DeleteTodoListQuery), request.RouteTodoListId, nameof(request.RouteTodoListId), _logger);

		TodoListModel? todoListDbModel = await _todoListRepository.GetAsync(request.RouteTodoListId);
		ExceptionsService.WhenEntityIsNullThrowCritical(nameof(DeleteTodoListQuery), todoListDbModel, _logger, request.RouteTodoListId);

		var todoListDto = _todoListMapper.TransferToDto(todoListDbModel!);
		var data = _todoListViewModelsFactory.CreateDeleteOutputVM(todoListDto);

		return new DeleteTodoListQueryResponse(data);
	}

	public async Task<DeleteTodoListCommandResponse> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(DeleteTodoListCommand), request.TodoListId, nameof(request.TodoListId), _logger);

		TodoListModel? todoListDbModel = await _todoListRepository.GetAsync(request.TodoListId);
		ExceptionsService.WhenEntityIsNullThrowCritical(nameof(DeleteTodoListCommand), todoListDbModel, _logger, request.TodoListId);

		ExceptionsService.WhenIdsAreNotEqualThrowCritical(nameof(DeleteTodoListCommand), todoListDbModel!.Id, nameof(todoListDbModel.Id), request.TodoListId, nameof(request.TodoListId), _logger);

		_todoListRepository.Remove(todoListDbModel);
		await _dataUnitOfWork.SaveChangesAsync();

		return new DeleteTodoListCommandResponse();
	}
}
