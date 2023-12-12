using App.Features.Boards.Common.Interfaces;
using App.Features.Exceptions.Throw;
using App.Features.Pagination;
using App.Features.Tasks.Common.Helpers;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Interfaces;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

		var extendedOfTasksCountsQuery = paginatedTodoListsQuery.Select(todoList =>
			Tuple.Create(
				todoList,
				todoList.Tasks.Count(task =>
					task.Status == TaskStatusHelper.TaskStatusType.Completed),
				todoList.Tasks.Count)
			);

		var tuples = await extendedOfTasksCountsQuery.ToListAsync();
		var tuplesDtos = TupleBrieflyMapper.MapToDto(tuples);
		int allTodoListsCount = await _todoListRepository.CountAsync(_predicateItemsOwner);

		var data = _boardsVMFactory.CreateBrieflyOutputVM(
			tuplesDtos,
			new PaginationData(
				request.PageNumber,
				request.ItemsPerPageCount,
				allTodoListsCount,
				_logger)
			);

		return new GetBoardBrieflyQueryResponse(data);
	}
}
