using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_DomainEntities;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.AppData;
using Project_Main.Models.Factories.ViewModels;
using Project_Main.Models.Outputs.ViewModels;
using Project_Main.Services.DTO;
using Project_Main.Services.Identity;

namespace Project_Main.Controllers
{
    public class BoardsController : Controller
	{
		private readonly IBoardViewModelsFactory _boardsVMFactory;
        private readonly ITodoListMapper _todoListMapper;
		private readonly ITodoListRepository _todoListRepository;
		private readonly string _signedInUserId;

		public BoardsController(IBoardViewModelsFactory boardsVMFactory, ITodoListMapper todoListMapper, ITodoListRepository todoListRepository, IAccountService accountService)
		{
			_boardsVMFactory = boardsVMFactory;
			_todoListMapper = todoListMapper;
			_todoListRepository = todoListRepository;
			_signedInUserId = accountService.GetSignedInUserId();
		}

		/// <summary>
		/// Action GET with custom route to show All To Do Lists.
		/// </summary>
		/// <returns>All To Do Lists.</returns>
		[HttpGet]
		[Route(CustomRoutes.MainBoardRoute)]
		[Authorize]
		public async Task<ActionResult<IBoardBrieflyOutputVM>> Briefly()
		{
			ICollection<TodoListModel> todoListModels = await _todoListRepository.GetAllWithDetailsByFilterAsync(todoList => todoList.UserId == _signedInUserId);
			var todoListDtos = _todoListMapper.TransferToDto(todoListModels);
			var brieflyOutputVM = _boardsVMFactory.CreateBrieflyOutputVM(todoListDtos);

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
		public async Task<ActionResult<IBoardAllOutputVM>> All()
		{
			var todoListModels = await _todoListRepository.GetAllWithDetailsAsync(_signedInUserId);
			var todoListDtos = _todoListMapper.TransferToDto(todoListModels);
			var allOutputVM = _boardsVMFactory.CreateAllOutputVM(todoListDtos);

			return View(allOutputVM);
		}
	}
}
