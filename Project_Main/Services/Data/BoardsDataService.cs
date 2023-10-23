using Project_DomainEntities;
using Project_Main.Infrastructure.DTOs;
using Project_Main.Models.DataBases.AppData;
using Project_Main.Models.ViewModels.OutputModels;
using Project_Main.Services.DTO;
using Project_Main.Services.Identity;

namespace Project_Main.Services.Data
{
	public class BoardsDataService : IBoardsDataService
	{
		private readonly ITodoListRepository _todoListRepository;
		private readonly IAccountService _accountService;

		public BoardsDataService(ITodoListRepository todoListRepository, IAccountService accountService)
		{
			_todoListRepository = todoListRepository;
			_accountService = accountService;
		}

		public async Task<BoardsBrieflyOutputVM> ImportDataForBrieflyBoardAsync()
		{
			var signedInUserId = _accountService.GetSignedInUserId();

			IEnumerable<ITodoListModel> userTodoLists = await _todoListRepository.GetAllWithDetailsAsync(signedInUserId);
			var userTodoListsDtos = TodoListDtoService.TransferToDto(userTodoLists);
			var brieflyOutputVM = TodoListDtoService.TransferToBoardsBrieflyOutputVM(userTodoListsDtos);

			return brieflyOutputVM;
		}

		public async Task<BoardsAllOutputVM> ImportDataForAllBoardAsync()
		{
			var signedInUserId = _accountService.GetSignedInUserId();
			var userTodoLists = await _todoListRepository.GetAllWithDetailsAsync(signedInUserId);

			IEnumerable<TodoListModelDto> userTodoListDtos = userTodoLists.Select(todoList => TodoListDtoService.TransferToDto(todoList));
			BoardsAllOutputVM boardsAllOutputVM = TodoListDtoService.TransferToBoardsAllOutputVM(userTodoListDtos);

			return boardsAllOutputVM;
		}
	}
}
