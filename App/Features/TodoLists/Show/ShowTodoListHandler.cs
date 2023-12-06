using App.Features.Exceptions.Throw;
using App.Features.TodoLists.Common.Interfaces;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Features.TodoLists.Show;

public class ShowTodoListHandler : IRequestHandler<ShowTodoListQuery, ShowTodoListQueryResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITodoListRepository _todoListRepository;
	private readonly ILogger<ShowTodoListHandler> _logger;
	private readonly ITodoListMapper _todoListMapper;
	private readonly ITodoListViewModelsFactory _todoListViewModelsFactory;

	public ShowTodoListHandler(IDataUnitOfWork dataUnitOfWork, ILogger<ShowTodoListHandler> logger, ITodoListMapper todoListMapper, ITodoListViewModelsFactory todoListViewModelsFactory)
	{
		_logger = logger;
		_dataUnitOfWork = dataUnitOfWork;
		_todoListRepository = _dataUnitOfWork.TodoListRepository;
		_todoListMapper = todoListMapper;
		_todoListViewModelsFactory = todoListViewModelsFactory;
	}

	public async Task<ShowTodoListQueryResponse> Handle(ShowTodoListQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(ShowTodoListQuery), request.TodoListId, nameof(request.TodoListId), _logger);

		var todoListDbModel = await _todoListRepository.GetWithDetails(request.TodoListId).SingleOrDefaultAsync();
		ExceptionsService.WhenEntityIsNullThrowCritical(nameof(ShowTodoListQuery), todoListDbModel, _logger, request.TodoListId);

		var todoListDto = _todoListMapper.TransferToDto(todoListDbModel!);
		var data = _todoListViewModelsFactory.CreateDetailsOutputVM(todoListDto, request.FilterDueDate);

		return new ShowTodoListQueryResponse(data);
	}
}
