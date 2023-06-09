﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Project_DomainEntities;
using Project_DomainEntities.Helpers;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.ViewModels.TodoListViewModels;
using Project_Main.Models.DataBases.AppData;
using Project_Main.Controllers.Helpers;

namespace Project_Main.Controllers
{
    /// <summary>
    /// Controller to manage To Do List actions based on certain routes.
    /// </summary>
    [Authorize]
	[Route(CustomRoutes.TodoListControllerRoute)]
	public class TodoListController : Controller
	{
		private readonly IDataUnitOfWork _dataUnitOfWork;
		private readonly ILogger<TodoListController> _logger;
		private readonly string controllerName = nameof(TodoListController);
		private string operationName = string.Empty;
		private const int DateCompareValueEarlier = 0;
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
			_logger = logger;
		}

		/// <summary>
		/// Action GET with custom route to show All To Do Lists.
		/// </summary>
		/// <returns>All To Do Lists.</returns>
		[HttpGet]
		[Route(CustomRoutes.MainBoardRoute, Name = CustomRoutes.MainBoardRouteName)]
		[Authorize]
		public async Task<IActionResult> Briefly()
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Briefly), controllerName);

			var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("Error with signed In User");
			var todoListRepo = _dataUnitOfWork.TodoListRepository;
			List<TodoListModel> todoLists = await todoListRepo.GetAllWithDetailsAsync(signedInUserId);

			if (todoLists == null)
			{
				_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(todoLists));
				return NotFound();
			}

			BrieflyViewModel brieflyViewModel = new()
			{
				TodoLists = todoLists
			};

			return View(brieflyViewModel);
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
		public async Task<ActionResult<IEnumerable<TodoListModel>>> All()
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(All), controllerName);

			var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var todoListRepo = _dataUnitOfWork.TodoListRepository;

			var allTodoLists = await todoListRepo.GetAllWithDetailsAsync(signedInUserId);

			if (allTodoLists == null)
			{
				_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(allTodoLists));
				return NotFound();
			}

			return View(allTodoLists);
		}

		/// <summary>
		/// Action GET with custom route to show specific To Do List with details.
		/// </summary>
		/// <param name="id">Target To Do List id.</param>
		/// <returns>Single To Do List with details.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
		[HttpGet]
		[Route(CustomRoutes.SingleTodoListDetailsRoute)]
		public async Task<IActionResult> SingleDetails(int id, DateTime? filterDueDate)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(SingleDetails), controllerName);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, id, nameof(id), HelperOther.idBoundryBottom, _logger);

			var todoListRepo = _dataUnitOfWork.TodoListRepository;
			var todoListFromDb = await todoListRepo.GetWithDetailsAsync(id);

			if (todoListFromDb == null)
			{
				_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(todoListFromDb));
				return NotFound();
			}

			DateTime todayDate = DateTime.Today;
			var allTodoListTasks = todoListFromDb.Tasks;

			TodoListViewModel todoListViewModel = new()
			{
				Id = todoListFromDb.Id,
				Name = todoListFromDb.Title,
				TasksForToday = allTodoListTasks.Where(t => t.DueDate.ToShortDateString() == todayDate.ToShortDateString() && t.Status != TaskStatusHelper.TaskStatusType.Completed).ToList(),
				TasksCompleted = allTodoListTasks.Where(t => t.Status == TaskStatusHelper.TaskStatusType.Completed && t.DueDate.CompareTo(todayDate) > DateCompareValueEarlier).ToList(),
				TasksNotCompleted = allTodoListTasks.Where(t =>
				{
					if (filterDueDate is null)
					{
						return t.Status != TaskStatusHelper.TaskStatusType.Completed && t.DueDate.CompareTo(todayDate) > DateCompareValueEarlier;
					}

					return (t.Status != TaskStatusHelper.TaskStatusType.Completed) && (t.DueDate.CompareTo(filterDueDate) < DateCompareValueEarlier && t.DueDate.CompareTo(todayDate) > DateCompareValueEarlier);

				}).ToList(),
				TasksExpired = allTodoListTasks.Where(t => t.DueDate.CompareTo(todayDate) < DateCompareValueEarlier).ToList()
			};

			todoListViewModel.TasksNotCompleted.Sort(new TasksComparer());
			todoListViewModel.TasksForToday.Sort(new TasksComparer());
			todoListViewModel.TasksCompleted.Sort(new TasksComparer());

			return View(todoListViewModel);
		}

		/// <summary>
		/// Action GET to create To Do List.
		/// </summary>
		/// <returns>Create view.</returns>
		[HttpGet]
		public IActionResult Create()
		{
			var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			
			return View(new TodoListModel()
			{
				UserId = signedInUserId
			});
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
				var todoListRepository = _dataUnitOfWork.TodoListRepository;

				if (await todoListRepository.DoesAnyExistWithSameNameAsync(todoListModel.Title))
				{
					ModelState.AddModelError(string.Empty, Messages.NameTaken);

					return View(todoListModel);
				}

				todoListModel.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				await todoListRepository.AddAsync(todoListModel);
				await _dataUnitOfWork.SaveChangesAsync();

				return RedirectToAction(nameof(Briefly));
			}

			return View(todoListModel);
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
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, id, nameof(id), HelperOther.idBoundryBottom, _logger);

			var todoListRepository = _dataUnitOfWork.TodoListRepository;
			var todoListModel = await todoListRepository.GetAsync(id);

			if (todoListModel == null)
			{
				_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(todoListModel));
				return NotFound();
			}

			return View(todoListModel);
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
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, id, nameof(id), HelperOther.idBoundryBottom, _logger);

			if (id != todoListModel.Id)
			{
				_logger.LogError(Messages.ConflictBetweenTodoListIdsAsParamAndFromModelObjectLogger, operationName, id, todoListModel.Id);
				return Conflict();
			}

			if (ModelState.IsValid)
			{
				var todoListRepository = _dataUnitOfWork.TodoListRepository;
				await todoListRepository.Update(todoListModel);
				await _dataUnitOfWork.SaveChangesAsync();

				return RedirectToAction(nameof(Briefly));
			}

			return View(todoListModel);
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
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, id, nameof(id), HelperOther.idBoundryBottom, _logger);

			var todoListRepository = _dataUnitOfWork.TodoListRepository;
			var todoListModel = await todoListRepository.GetAsync(id);

			if (todoListModel == null)
			{
				_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(todoListModel));
				return NotFound();
			}

			DeleteListViewModel deleteViewModel = new()
			{
				ListModel = todoListModel,
				TasksCount = todoListModel.Tasks.Count
			};

			return View(deleteViewModel);
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
		[HttpPost, ActionName("Delete")]
		[Route(CustomRoutes.TodoListDeleteRoute)]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, id, nameof(id), HelperOther.idBoundryBottom, _logger);

			if (ModelState.IsValid)
			{
				var todoListRepository = _dataUnitOfWork.TodoListRepository;
				var todoListModel = await todoListRepository.GetAsync(id);

				if (todoListModel != null)
				{
					if (todoListModel.Id != id)
					{
						_logger.LogError(Messages.ConflictBetweenTodoListIdsAsParamAndFromModelObjectLogger, operationName, id, todoListModel.Id);
						return Conflict();
					}

					await todoListRepository.Remove(todoListModel);
					await _dataUnitOfWork.SaveChangesAsync();

					return RedirectToAction(nameof(Briefly));
				}
			}

			return View(nameof(Delete));
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
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), HelperOther.idBoundryBottom, _logger);

			var todoListRepository = _dataUnitOfWork.TodoListRepository;
			await todoListRepository.DuplicateWithDetailsAsync(todoListId);
			await _dataUnitOfWork.SaveChangesAsync();

			return RedirectToAction(nameof(Briefly));
		}
	}
}
