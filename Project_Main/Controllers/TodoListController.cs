using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Project_DomainEntities;
using Project_DomainEntities.Helpers;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.Repositories;
using Project_Main.Models.ViewModels.TodoListViewModels;
using Microsoft.EntityFrameworkCore;

namespace Project_Main.Controllers
{
    /// <summary>
    /// Controller to manage To Do List actions based on certain routes.
    /// </summary>
    [Authorize]
	[Route("TodoList")]
	public class TodoListController : Controller
	{
		private readonly IDataUnitOfWork _dataUnitOfWork;
		private readonly ILogger<TodoListController> _logger;
		private readonly string controllerName = nameof(TodoListController);
		private string operationName = string.Empty;
		private const int DateCompareValueEarlier = 0;

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
		[Route("All/Briefly")]
		[Authorize]
		public async Task<IActionResult> Briefly()
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Briefly), controllerName);

			var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (signedInUserId is null)
			{
				throw new InvalidOperationException("Error with signed In User");
			}

			var todoListRepo = _dataUnitOfWork.TodoListRepository;
			List<TodoListModel> todoLists = await todoListRepo.GetAllWithDetailsAsync(signedInUserId);
			//List<TodoListModel> todoLists = await _context.GetAllTodoListsWithDetailsAsync(signedInUserId);

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
		[Route("All/Details")]
		public async Task<ActionResult<IEnumerable<TodoListModel>>> All()
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(All), controllerName);

			var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var todoListRepo = _dataUnitOfWork.TodoListRepository;

			// RETURNING only those for current user, because this feature will be remove, app won't provide all tasks from db at once
			var allTodoLists = await todoListRepo.GetAllWithDetailsAsync(signedInUserId);
			//var allTodoLists = await _context.GetAllTodoListsWithDetailsAsync(signedInUserId);

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
		[Route("{id:int}/SingleDetails")]
		public async Task<IActionResult> SingleDetails(int id, DateTime? filterDueDate)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(SingleDetails), controllerName);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, id, nameof(id), HelperOther.idBoundryBottom, _logger);

			//var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var todoListRepo = _dataUnitOfWork.TodoListRepository;
			var todoListModel = await todoListRepo.GetAsync(id);
			//var todoListModel = await _context.GetTodoListWithDetailsAsync(id, signedInUserId);

			if (todoListModel == null)
			{
				_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(todoListModel));
				return NotFound();
			}

			DateTime todayDate = DateTime.Today;
			var allTodoListTasks = todoListModel.Tasks;

			TodoListViewModel todoListViewModel = new()
			{
				Id = todoListModel.Id,
				Name = todoListModel.Name,
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
		public async Task<IActionResult> Create([Bind("Name, UserId")] TodoListModel todoListModel)
		{
			if (ModelState.IsValid)
			{
				var todoListRepository = _dataUnitOfWork.TodoListRepository;

				if (await todoListRepository.DoesAnyExistWithSameNameAsync(todoListModel.Name))
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
		[Route("{id:int}/Edit")]
		public async Task<IActionResult> Edit(int id)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Edit), controllerName);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, id, nameof(id), HelperOther.idBoundryBottom, _logger);

			var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var todoListRepository = _dataUnitOfWork.TodoListRepository;
			var todoListModel = await todoListRepository.GetAsync(id);
			//var todoListModel = await _context.GetTodoListAsync(id, signedInUserId);

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
		[Route("{id:int}/Edit")]
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
				todoListRepository.Update(todoListModel);
				await _dataUnitOfWork.SaveChangesAsync();
				//await _context.UpdateTodoListAsync(todoListModel);

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
		[Route("{id:int}/Delete")]
		public async Task<IActionResult> Delete(int id)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Delete), controllerName);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, id, nameof(id), HelperOther.idBoundryBottom, _logger);

			//var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var todoListRepository = _dataUnitOfWork.TodoListRepository;
			var todoListModel = await todoListRepository.GetAsync(id);
			//var todoListModel = await _context.GetTodoListWithDetailsAsync(id, signedInUserId);

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
		[Route("{id:int}/Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, id, nameof(id), HelperOther.idBoundryBottom, _logger);

			if (ModelState.IsValid)
			{
				//var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var todoListRepository = _dataUnitOfWork.TodoListRepository;
				var todoListModel = await todoListRepository.GetAsync(id);
				//var todoListModel = await _context.GetTodoListAsync(id, signedInUserId);

				if (todoListModel != null)
				{
					if (todoListModel.Id != id)
					{
						_logger.LogError(Messages.ConflictBetweenTodoListIdsAsParamAndFromModelObjectLogger, operationName, id, todoListModel.Id);
						return Conflict();
					}

					todoListRepository.Remove(todoListModel);
					await _dataUnitOfWork.SaveChangesAsync();

					//await _context.DeleteTodoListAsync(todoListModel.Id, signedInUserId);
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
		[Route("{todoListId:int}/Duplicate")]
		public async Task<IActionResult> Duplicate(int todoListId)
		{
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), HelperOther.idBoundryBottom, _logger);

			//var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var todoListRepository = _dataUnitOfWork.TodoListRepository;
			await todoListRepository.DuplicateWithDetailsAsync(todoListId);
			await _dataUnitOfWork.SaveChangesAsync();

			//await _context.DuplicateTodoListWithDetailsAsync(todoListId, signedInUserId);

			return RedirectToAction(nameof(Briefly));
		}
	}
}
