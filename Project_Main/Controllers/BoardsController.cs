using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.AppData;
using Project_Main.Models.ViewModels.OutputModels;
using Project_Main.Services.Data;

namespace Project_Main.Controllers
{
    public class BoardsController : Controller
	{
		private readonly IDataUnitOfWork _dataUnitOfWork;
		private readonly ITodoListRepository _todoListRepository;
		private readonly IBoardsDataService _boardsDataService;

		public BoardsController(IDataUnitOfWork dataUnitOfWork, IBoardsDataService boardsDataService)
		{
			_dataUnitOfWork = dataUnitOfWork;
			_todoListRepository = _dataUnitOfWork.TodoListRepository;
			_boardsDataService = boardsDataService;
		}

		/// <summary>
		/// Action GET with custom route to show All To Do Lists.
		/// </summary>
		/// <returns>All To Do Lists.</returns>
		[HttpGet]
		[Route(CustomRoutes.MainBoardRoute)]
		[Authorize]
		public async Task<ActionResult<BoardsBrieflyOutputVM>> Briefly()
		{
			var brieflyOutputVM = await _boardsDataService.ImportDataForBrieflyBoardAsync();

			return View(brieflyOutputVM);
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
		public async Task<ActionResult<BoardsAllOutputVM>> All()
		{
			var boardsAllOutputVM = await _boardsDataService.ImportDataForAllBoardAsync();

			return View(boardsAllOutputVM);
		}
	}
}
