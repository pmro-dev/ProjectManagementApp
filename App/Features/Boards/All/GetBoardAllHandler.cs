using App.Common.Helpers;
using App.Features.Boards.All.Interfaces;
using App.Features.Boards.Common.Interfaces;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.Users.Common.Interfaces;
using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Boards.All;

public class GetBoardAllHandler : IRequestHandler<GetBoardAllQuery, IBoardAllOutputVM>
{
	private readonly ILogger<GetBoardAllHandler> _logger;
	private readonly IBoardViewModelsFactory _boardsVMFactory;
	private readonly ITodoListMapper _todoListMapper;
	private readonly ITodoListRepository _todoListRepository;
	private readonly string _signedInUserId;

	public GetBoardAllHandler(IBoardViewModelsFactory boardsVMFactory, ITodoListMapper todoListMapper, ITodoListRepository todoListRepository, IUserService userService, ILogger<GetBoardAllHandler> logger)
	{
		_logger = logger;
		_boardsVMFactory = boardsVMFactory;
		_todoListMapper = todoListMapper;
		_todoListRepository = todoListRepository;
		_signedInUserId = userService.GetSignedInUserId();

		ExceptionsService.WhenPropertyIsNullOrEmptyThrowCritical("Constructing " + nameof(GetBoardAllHandler), _signedInUserId, nameof(_signedInUserId), _logger);
	}

	public async Task<IBoardAllOutputVM> Handle(GetBoardAllQuery request, CancellationToken cancellationToken)
	{
		var todoListModels = await _todoListRepository.GetAllWithDetailsAsync(_signedInUserId);
		var todoListDtos = _todoListMapper.TransferToDto(todoListModels);
		IBoardAllOutputVM allOutputVM = _boardsVMFactory.CreateAllOutputVM(todoListDtos);

		return allOutputVM;
	}
}
