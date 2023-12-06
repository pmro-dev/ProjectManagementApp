using App.Features.Boards.Common.Interfaces;
using App.Features.Exceptions.Throw;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Interfaces;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Features.Boards.Briefly;

public class GetBoardBrieflyHandler : IRequestHandler<GetBoardBrieflyQuery, GetBoardBrieflyQueryResponse>
{
	private readonly ILogger<GetBoardBrieflyHandler> _logger;
	private readonly IBoardViewModelsFactory _boardsVMFactory;
	private readonly ITodoListMapper _todoListMapper;
	private readonly ITodoListRepository _todoListRepository;
	private readonly string _signedInUserId;

	public GetBoardBrieflyHandler(IBoardViewModelsFactory boardsVMFactory, ITodoListMapper todoListMapper, ITodoListRepository todoListRepository, 
		IUserService userService, ILogger<GetBoardBrieflyHandler> logger)
	{
		_logger = logger;
		_boardsVMFactory = boardsVMFactory;
		_todoListMapper = todoListMapper;
		_todoListRepository = todoListRepository;
		_signedInUserId = userService.GetSignedInUserId();

		ExceptionsService.WhenPropertyIsNullOrEmptyThrow("Constructing " + nameof(GetBoardBrieflyHandler), _signedInUserId, nameof(_signedInUserId), _logger);
	}

	public async Task<GetBoardBrieflyQueryResponse> Handle(GetBoardBrieflyQuery request, CancellationToken cancellationToken)
	{
		ICollection<TodoListModel> todoListModels = await _todoListRepository
			.GetAllWithDetailsByFilter(todoList => todoList.UserId == _signedInUserId)
			.ToListAsync();

		var todoListDtos = _todoListMapper.TransferToDto(todoListModels);
		var data = _boardsVMFactory.CreateBrieflyOutputVM(todoListDtos);

		return new GetBoardBrieflyQueryResponse(data);
	}
}
