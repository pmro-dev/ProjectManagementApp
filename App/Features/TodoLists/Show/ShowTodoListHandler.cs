using App.Features.Exceptions.Throw;
using App.Features.Pagination;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.TodoLists.Show;

public class ShowTodoListHandler : IRequestHandler<ShowTodoListQuery, ShowTodoListQueryResponse>
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITodoListRepository _todoListRepository;
	private readonly ILogger<ShowTodoListHandler> _logger;
	private readonly ITodoListMapper _todoListMapper;
	private readonly ITodoListViewModelsFactory _todoListViewModelsFactory;

	public ShowTodoListHandler(IDataUnitOfWork dataUnitOfWork, ILogger<ShowTodoListHandler> logger, 
		ITodoListMapper todoListMapper, ITodoListViewModelsFactory todoListViewModelsFactory)
	{
		_logger = logger;
		_dataUnitOfWork = dataUnitOfWork;
		_todoListRepository = _dataUnitOfWork.TodoListRepository;
		_todoListMapper = todoListMapper;
		_todoListViewModelsFactory = todoListViewModelsFactory;
	}

	public async Task<ShowTodoListQueryResponse> Handle(ShowTodoListQuery request, CancellationToken cancellationToken)
	{
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(ShowTodoListQuery), request.TodoListId, nameof(request.TodoListId), _logger);

		TodoListModel? todoListDbModel = await _todoListRepository.GetSingleWithDetailsAsync(request.TodoListId, request.OrderDetailsBySelector, request.PageNumber, request.ItemsPerPageCount);
		ExceptionsService.WhenEntityIsNullThrowCritical(nameof(ShowTodoListQuery), todoListDbModel, _logger, request.TodoListId);

		var todoListDto = _todoListMapper.TransferToDto(todoListDbModel!);

		PaginationData paginData = new(request.PageNumber, request.ItemsPerPageCount, todoListDto.Tasks.Count, _logger);

		var data = _todoListViewModelsFactory.CreateDetailsOutputVM(todoListDto, paginData, request.FilterDueDate);

		return new ShowTodoListQueryResponse(data);
	}
}
