using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static App.Common.Views.ViewsConsts;
using static App.Common.ControllersConsts;
using App.Features.TodoLists.Common.Interfaces;
using App.Infrastructure.Databases.App.Interfaces;
using App.Features.TodoLists.Edit;
using App.Infrastructure;
using App.Features.TodoLists.Create;
using App.Infrastructure.Helpers;
using App.Common.Helpers;
using App.Common.ViewModels;
using App.Features.TodoLists.Common.Models;

namespace App.Features.TodoLists
{
	/// <summary>
	/// Controller to manage To Do List actions based on specific routes.
	/// </summary>
	[Authorize]
	public class TodoListController : Controller
	{
		private readonly IDataUnitOfWork _dataUnitOfWork;
		private readonly ITodoListRepository _todoListRepository;
		private readonly ILogger<TodoListController> _logger;
		private readonly ITodoListMapper _todoListMapper;
		private readonly ITodoListViewModelsFactory _todoListViewModelsFactory;

		public static string ShortName { get; } = nameof(TodoListController).Replace("Controller", string.Empty);

		/// <summary>
		/// Initializes controller with DbContext and Logger.
		/// </summary>
		/// <param name="context">Database context.</param>
		/// <param name="logger">Logger provider.</param>
		public TodoListController(IDataUnitOfWork dataUnitOfWork, ILogger<TodoListController> logger, ITodoListMapper todoListMapper, ITodoListViewModelsFactory todoListViewModelsFactory)
		{
			_dataUnitOfWork = dataUnitOfWork;
			_todoListRepository = _dataUnitOfWork.TodoListRepository;
			_logger = logger;
			_todoListMapper = todoListMapper;
			_todoListViewModelsFactory = todoListViewModelsFactory;
		}

		/// <summary>
		/// Action GET to create To Do List.
		/// </summary>
		/// <returns>Create view.</returns>
		[HttpGet]
		[Route(CustomRoutes.TodoListCreateRoute)]
		public IActionResult Create()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			TodoListCreateOutputVM createOutputVM = _todoListViewModelsFactory.CreateCreateOutputVM(userId);
			WrapperViewModel<TodoListCreateInputVM, TodoListCreateOutputVM> createWrapperVM = _todoListViewModelsFactory.CreateWrapperCreateVM();
			createWrapperVM.OutputVM = createOutputVM;

			return View(TodoListViews.Create, createWrapperVM);
		}

		/// <summary>
		/// Action POST to create To Do List.
		/// </summary>
		/// <param name="todoListModel">Model with form's data.</param>
		/// <returns>Return different view based on the final result. Redirect to Briefly or to view with form.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(WrapperViewModel<TodoListCreateInputVM, TodoListCreateOutputVM> createWrapperVM)
		{
			if (!ModelState.IsValid) return View(TodoListViews.Create, createWrapperVM);

			var todoListDto = _todoListMapper.TransferToDto(createWrapperVM.InputVM);

			if (await _todoListRepository.CheckThatAnyWithSameNameExistAsync(todoListDto.Title))
			{
				ModelState.AddModelError(string.Empty, MessagesPacket.NameTaken);
				return View(createWrapperVM);
			}

			todoListDto.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var todoListModel = _todoListMapper.TransferToModel(todoListDto);
			await _todoListRepository.AddAsync(todoListModel);
			await _dataUnitOfWork.SaveChangesAsync();

			return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);
		}

		/// <summary>
		/// Action GET to EDIT To Do List.
		/// </summary>
		/// <param name="id">Target To Do List id.</param>
		/// <returns>Return different view based on the final result. Return Bad Request when given id is invalid, Return Not Found when there isn't such To Do List in Db or return edit view.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
		[HttpGet]
		[Route(CustomRoutes.TodoListEditRoute)]
		public async Task<IActionResult> Edit(int id)
		{
			ExceptionsService.ThrowExceptionWhenIdLowerThanBottomBoundry(nameof(Edit), id, nameof(id), _logger);

			TodoListModel? todoListModel = await _todoListRepository.GetAsync(id);

			if (todoListModel == null)
			{
				_logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, nameof(Edit), nameof(TodoListModel), id);
				return NotFound();
			}

			var todoListDto = _todoListMapper.TransferToDto(todoListModel);
			var editOutputVM = _todoListViewModelsFactory.CreateEditOutputVM(todoListDto);
			var editWrapperVM = _todoListViewModelsFactory.CreateWrapperEditVM();
			editWrapperVM.OutputVM = editOutputVM;

			return View(TodoListViews.Edit, editWrapperVM);
		}

		/// <summary>
		/// Action POST to EDIT To Do List.
		/// </summary>
		/// <param name="id">Target To Do List id.</param>
		/// <param name="todoListModel">Model with form's data.</param>
		/// <returns>
		/// Return different view based on the final result. Return Bad Request when given id is invalid or id is not equal to model id, 
		/// Redirect to index view when updating operation succeed.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
		[HttpPost]
		[Route(CustomRoutes.TodoListEditRoute)]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [FromForm] WrapperViewModel<TodoListEditInputVM, TodoListEditOutputVM> editWrapperVM)
		{
			if (!ModelState.IsValid) return View(TodoListViews.Edit, editWrapperVM);

			ExceptionsService.ThrowExceptionWhenIdLowerThanBottomBoundry(nameof(Edit), id, nameof(id), _logger);

			var todoListId = editWrapperVM.OutputVM.Id;

			ExceptionsService.ThrowWhenIdsAreNotEqual(nameof(Edit), id, nameof(id), todoListId, nameof(todoListId), _logger);

			TodoListModel? todoListDbModel = await _todoListRepository.GetAsync(todoListId);

			if (todoListDbModel is null)
			{
				_logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, nameof(Edit), nameof(TodoListModel), todoListId);
				return NotFound();
			}

			var editInputDto = _todoListMapper.TransferToDto(editWrapperVM.InputVM);
			_todoListMapper.UpdateModel(todoListDbModel, editInputDto);

			_todoListRepository.Update(todoListDbModel);
			await _dataUnitOfWork.SaveChangesAsync();

			return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);
		}

		/// <summary>
		/// Action GET to DELETE To Do List.
		/// </summary>
		/// <param name="id">Target To Do List id.</param>
		/// <returns>
		/// Return different view based on the final result. 
		/// Not Found when there isn't such To Do List in Db or return view when delete operation succeed.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
		[HttpGet]
		[Route(CustomRoutes.TodoListDeleteRoute)]
		public async Task<IActionResult> Delete(int id)
		{
			ExceptionsService.ThrowExceptionWhenIdLowerThanBottomBoundry(nameof(Delete), id, nameof(id), _logger);

			TodoListModel? todoListDbModel = await _todoListRepository.GetAsync(id);

			if (todoListDbModel == null)
			{
				_logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, nameof(Delete), nameof(TodoListModel), id);
				return NotFound();
			}

			var todoListDto = _todoListMapper.TransferToDto(todoListDbModel);
			var deleteOutputVM = _todoListViewModelsFactory.CreateDeleteOutputVM(todoListDto);

			return View(TodoListViews.Delete, deleteOutputVM);
		}

		/// <summary>
		/// Action POST to DELETE To Do List.
		/// </summary>
		/// <param name="id">Target To Do List id.</param>
		/// <returns>
		/// Return different view based on the final result. 
		/// Return Conflict when given id and To Do List id of object from Database are not equal, 
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
		[HttpPost]
		[Route(CustomRoutes.TodoListDeletePostRoute)]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePost(int id)
		{
			ExceptionsService.ThrowExceptionWhenIdLowerThanBottomBoundry(nameof(DeletePost), id, nameof(id), _logger);

			if (ModelState.IsValid)
			{
				TodoListModel? todoListDbModel = await _todoListRepository.GetAsync(id);

				if (todoListDbModel is null)
				{
					ExceptionsService.ThrowEntityNotFoundInDb(nameof(DeletePost), nameof(TodoListModel), id.ToString(), _logger);
				}

				ExceptionsService.ThrowWhenIdsAreNotEqual(nameof(DeletePost), todoListDbModel!.Id, nameof(todoListDbModel.Id), id, nameof(id), _logger);

				_todoListRepository.Remove(todoListDbModel);
				await _dataUnitOfWork.SaveChangesAsync();

				return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);
			}

			return View(TodoListCtrl.DeleteAction);
		}

		/// <summary>
		/// Action POST to Duplicate certain To Do List with details.
		/// </summary>
		/// <param name="todoListId">Target To Do List to duplicate.</param>
		/// <returns>Redirect to view.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when To Do List's id is out of range.</exception>
		[Route(CustomRoutes.TodoListDuplicateRoute)]
		public async Task<IActionResult> Duplicate(int todoListId)
		{
			ExceptionsService.ThrowExceptionWhenIdLowerThanBottomBoundry(nameof(Duplicate), todoListId, nameof(todoListId), _logger);

			await _todoListRepository.DuplicateWithDetailsAsync(todoListId);
			await _dataUnitOfWork.SaveChangesAsync();

			return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);
		}


		/// <summary>
		/// Action GET with custom route to show specific To Do List with details.
		/// </summary>
		/// <param name="id">Target To Do List id.</param>
		/// <returns>Single To Do List with details.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
		[HttpGet]
		[Route(CustomRoutes.TodoListShowRoute)]
		public async Task<IActionResult> Show(int id, DateTime? filterDueDate)
		{
			ExceptionsService.ThrowExceptionWhenIdLowerThanBottomBoundry(nameof(Show), id, nameof(id), _logger);

			TodoListModel? todoListDbModel = await _todoListRepository.GetWithDetailsAsync(id);

			if (todoListDbModel is null)
			{
				_logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, nameof(Show), nameof(TodoListModel), id);
				return NotFound();
			}

			var todoListDto = _todoListMapper.TransferToDto(todoListDbModel);
			var detailsOutputVM = _todoListViewModelsFactory.CreateDetailsOutputVM(todoListDto, filterDueDate);

			return View(TodoListViews.Show, detailsOutputVM);
		}
	}
}
