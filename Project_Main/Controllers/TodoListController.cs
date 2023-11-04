using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Project_DomainEntities;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.AppData;
using Project_Main.Models.DataBases.Helpers;
using Project_Main.Models.ViewModels.OutputModels;
using Project_Main.Infrastructure.DTOs;
using Project_Main.Services.DTO;

namespace Project_Main.Controllers
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
		private readonly string controllerName = nameof(TodoListController);
		private string operationName = string.Empty;
		private const string todoListDataToBind = "Title, UserId";

		public static string ShortName { get; } = nameof(TodoListController).Replace("Controller", string.Empty);

		/// <summary>
		/// Initializes controller with DbContext and Logger.
		/// </summary>
		/// <param name="context">Database context.</param>
		/// <param name="logger">Logger provider.</param>
		public TodoListController(IDataUnitOfWork dataUnitOfWork, ILogger<TodoListController> logger)
		{
			_dataUnitOfWork = dataUnitOfWork;
			_todoListRepository = _dataUnitOfWork.TodoListRepository;
			_logger = logger;
		}

		/// <summary>
		/// Action GET to create To Do List.
		/// </summary>
		/// <returns>Create view.</returns>
		[HttpGet]
		[Route(CustomRoutes.TodoListCreateRoute)]
		public IActionResult Create()
		{
			var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var viewModel = new TodoListModel()
			{
				UserId = signedInUserId
			};

			return View(TodoListViews.Create, viewModel);
		}

		/// <summary>
		/// Action POST to create To Do List.
		/// </summary>
		/// <param name="todoListModel">Model with form's data.</param>
		/// <returns>Return different view based on the final result. Redirect to Briefly or to view with form.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind(todoListDataToBind)] TodoListModel todoListModel)
		{
			if (ModelState.IsValid)
			{
				if (await _todoListRepository.DoesAnyExistWithSameNameAsync(todoListModel.Title))
				{
                    ModelState.AddModelError(string.Empty, MessagesPacket.NameTaken);

					return View(todoListModel);
				}

				todoListModel.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				await _todoListRepository.AddAsync(todoListModel);
				await _dataUnitOfWork.SaveChangesAsync();

				return RedirectToAction(BoardsCtrl.BrieflyAction);
			}

			return View(TodoListViews.Create, todoListModel);
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
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Edit), controllerName);
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

			var todoListModel = await _todoListRepository.GetAsync(id);

			if (todoListModel == null)
			{
                _logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, id, HelperDatabase.TodoListsDbSetName);
				return NotFound();
			}

			return View(TodoListViews.Edit, todoListModel);
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
		public async Task<IActionResult> Edit(int id, TodoListModel todoListModel)
		{
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

			if (id != todoListModel.Id)
			{
                _logger.LogError(MessagesPacket.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, id, todoListId);
				return Conflict();
			}

			if (ModelState.IsValid)
			{
				_todoListRepository.Update(todoListModel);
				await _dataUnitOfWork.SaveChangesAsync();

				return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);
			}

			return View(TodoListViews.Edit, todoListModel);
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
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Delete), controllerName);
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

			var todoListModel = await _todoListRepository.GetAsync(id);

			if (todoListModel == null)
			{
                _logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, id, HelperDatabase.TodoListsDbSetName);
				return NotFound();
			}

			TodoListDeleteOutputVM todoListDeleteOutputVM = new()
			{
				ListModel = todoListModel,
				TasksCount = todoListModel.Tasks.Count()
			};

			return View(TodoListViews.Delete, todoListDeleteOutputVM);
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
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

			if (ModelState.IsValid)
			{
				var todoListModel = await _todoListRepository.GetAsync(id);

				if (todoListModel != null)
				{
					if (todoListModel.Id != id)
					{
                        _logger.LogError(MessagesPacket.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, id, todoListDbModel.Id);
						return Conflict();
					}

					_todoListRepository.Remove(todoListModel);
					await _dataUnitOfWork.SaveChangesAsync();

					return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);
				}
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
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, todoListId, nameof(todoListId), HelperCheck.IdBottomBoundry, _logger);

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
		[Route(CustomRoutes.TodoListDetailsRoute)]
		public async Task<IActionResult> TodoListDetails(int id, DateTime? filterDueDate)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(TodoListDetails), controllerName);
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

			var todoListFromDb = await _todoListRepository.GetWithDetailsAsync(id);

			if (todoListFromDb is null)
			{
                _logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, id, HelperDatabase.TodoListsDbSetName);
				return NotFound();
			}

			TodoListModelDto todoListModelDto = TodoListDtoService.TransferToDto(todoListFromDb);
			BoardsSingleDetailsOutputVM singleDetailsVM = TodoListDtoService.TransferToSingleDetailsOutputVM(todoListModelDto, filterDueDate);

			var tasksComparer = new TasksComparer();

			var tasks = new Task[]
			{
				Task.Run(() => singleDetailsVM.TasksNotCompleted.Sort(tasksComparer)),
				Task.Run(() => singleDetailsVM.TasksForToday.Sort(tasksComparer)),
				Task.Run(() => singleDetailsVM.TasksCompleted.Sort(tasksComparer)),
			};

			Task.WaitAll(tasks);

			return View(TodoListViews.Details, singleDetailsVM);
		}
	}
}
