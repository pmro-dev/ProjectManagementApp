using App.Features.Boards.Common.Interfaces;
using App.Features.Exceptions.Throw;
using App.Features.Tasks.Common.Helpers;
using App.Features.Users.Common.Interfaces;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Features.Boards.Briefly;

public class GetBoardBrieflyHandler : IRequestHandler<GetBoardBrieflyQuery, GetBoardBrieflyQueryResponse>
{
	private readonly ILogger<GetBoardBrieflyHandler> _logger;
	private readonly IBoardViewModelsFactory _boardsVMFactory;
	private readonly ITodoListRepository _todoListRepository;
	private readonly string _signedInUserId;

	public GetBoardBrieflyHandler(IBoardViewModelsFactory boardsVMFactory, ITodoListRepository todoListRepository, IUserService userService, 
		ILogger<GetBoardBrieflyHandler> logger)
	{
		_logger = logger;
		_boardsVMFactory = boardsVMFactory;
		_todoListRepository = todoListRepository;
		_signedInUserId = userService.GetSignedInUserId();

		ExceptionsService.WhenPropertyIsNullOrEmptyThrow(nameof(GetBoardBrieflyHandler), _signedInUserId, nameof(_signedInUserId), _logger);
	}

	public async Task<GetBoardBrieflyQueryResponse> Handle(GetBoardBrieflyQuery request, CancellationToken cancellationToken)
	{
		var paginatedTodoListsQuery = _todoListRepository
			.GetMultipleByFilter(todoList =>
				todoList.UserId == _signedInUserId,
				todoList => todoList.Title,
				request.PageNumber,
				request.ItemsPerPageCount);

		var extendedOfTasksCountsQuery = paginatedTodoListsQuery.Select(todoList =>
			Tuple.Create(
				todoList,
				todoList.Tasks.Count(task => 
					task.Status == TaskStatusHelper.TaskStatusType.Completed),
				todoList.Tasks.Count
			));

		var tuples = await extendedOfTasksCountsQuery.ToListAsync();
		var tuplesDto = TupleBrieflyMapper.MapToDto(tuples);

		var data = _boardsVMFactory.CreateBrieflyOutputVM(tuplesDto);
		
		return new GetBoardBrieflyQueryResponse(data);
	}
}
