using App.Features.Boards.Common.Interfaces;
using App.Features.Exceptions.Throw;
using App.Features.Pagination;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Interfaces;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static App.Features.Tasks.Common.Helpers.TaskStatusHelper;

namespace App.Features.Boards.Briefly;

public class GetBoardBrieflyHandler : IRequestHandler<GetBoardBrieflyQuery, GetBoardBrieflyQueryResponse>
{
	private readonly ILogger<GetBoardBrieflyHandler> _logger;
	private readonly IBoardViewModelsFactory _boardsVMFactory;
	private readonly ITodoListRepository _todoListRepository;
	private readonly string _signedInUserId;
	private readonly Expression<Func<TodoListModel, bool>> _predicateItemsOwner;

	public GetBoardBrieflyHandler(IBoardViewModelsFactory boardsVMFactory, ITodoListRepository todoListRepository, IUserService userService,
		ILogger<GetBoardBrieflyHandler> logger)
	{
		_logger = logger;
		_boardsVMFactory = boardsVMFactory;
		_todoListRepository = todoListRepository;
		_signedInUserId = userService.GetSignedInUserId();
		ExceptionsService.WhenPropertyIsNullOrEmptyThrow(nameof(GetBoardBrieflyHandler), _signedInUserId, nameof(_signedInUserId), _logger);
		_predicateItemsOwner = todoList => todoList.UserId == _signedInUserId;
	}

	public async Task<GetBoardBrieflyQueryResponse> Handle(GetBoardBrieflyQuery request, CancellationToken cancellationToken)
	{
		var paginatedTodoListsQuery = _todoListRepository
			.GetMultipleByFilter(
				_predicateItemsOwner,
				request.OrderBySelector,
				request.PageNumber,
				request.ItemsPerPageCount);

		var extendedQuery = paginatedTodoListsQuery.Select(todoList =>
			Tuple.Create(
				todoList,
				todoList.Tasks
					.Count(task => task.Status == TaskStatusType.Completed),
				todoList.Tasks.Count)
			);

		var tuples = await extendedQuery.ToListAsync();
		var tuplesDto = TupleBrieflyMapper.MapToDto(tuples);
		int userTodoListsCount = await _todoListRepository.CountAsync(_predicateItemsOwner);

		var data = _boardsVMFactory.CreateBrieflyOutputVM(
			tuplesDto,
			new PaginationData(
				request.PageNumber,
				request.ItemsPerPageCount,
				userTodoListsCount,
				_logger)
			);

		return new GetBoardBrieflyQueryResponse(data);
	}
}
