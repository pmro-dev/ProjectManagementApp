using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_DomainEntities;
using Project_Main.Infrastructure.DTOs;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.AppData;
using Project_Main.Models.DataBases.Helpers;
using Project_Main.Models.ViewModels.OutputModels;
using Project_Main.Services;
using System.Security.Claims;

namespace Project_Main.Controllers
{
    public class BoardsController : Controller
	{
		private readonly IDataUnitOfWork _dataUnitOfWork;
		private readonly ITodoListRepository _todoListRepository;
		private readonly ILogger<BoardsController> _logger;
		private string operationName = string.Empty;
		private readonly string controllerName = nameof(BoardsController);
		public static string ShortName { get; } = nameof(BoardsController).Replace("Controller", string.Empty);

		public BoardsController(IDataUnitOfWork dataUnitOfWork, ILogger<BoardsController> logger)
		{
			_dataUnitOfWork = dataUnitOfWork;
			_todoListRepository = _dataUnitOfWork.TodoListRepository;
			_logger = logger;
		}

		/// <summary>
		/// Action GET with custom route to show All To Do Lists.
		/// </summary>
		/// <returns>All To Do Lists.</returns>
		[HttpGet]
		[Route(CustomRoutes.MainBoardRoute, Name = CustomRoutes.MainBoardRouteName)]
		[Authorize]
		public async Task<ActionResult<BoardsBrieflyOutputVM>> Briefly()
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Briefly), controllerName);

			var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("Error with signed In User");
			List<TodoListModel> todoLists = await _todoListRepository.GetAllWithDetailsAsync(signedInUserId);

			BoardsBrieflyOutputVM brieflyVM = new() { TodoLists = todoLists };

			return View(brieflyVM);
		}

		/// <summary>
		/// Action GET to (get) ALL To Do Lists.
		/// </summary>
		/// <returns>
		/// Return different view based on the final result. 
		/// Return: Not Found when there isn't any object for To Do Lists,
		/// or view with data.
		/// </returns>
		[HttpGet]
		[Route(CustomRoutes.AllDetailsRoute)]
		public async Task<ActionResult<IEnumerable<BoardsAllOutputVM>>> All()
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(All), controllerName);

			var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var allTodoLists = await _todoListRepository.GetAllWithDetailsAsync(signedInUserId);

			IEnumerable<TodoListModelDto> allTodoListsDto = allTodoLists.Select(todoList => TodoListDtoService.TransferToDto(todoList));

			IEnumerable<BoardsAllOutputVM> todoListsAllVM = allTodoListsDto.Select(todoListDto => TodoListDtoService.TransferToBoardsAllOutputVM(todoListDto));

			return View(todoListsAllVM);
		}
	}
}
