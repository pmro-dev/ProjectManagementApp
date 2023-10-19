using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_DomainEntities;
using Project_DomainEntities.Helpers;
using Project_Main.Infrastructure.Helpers;
using Project_Main.Models.DataBases.AppData;
using Project_Main.Models.DataBases.Helpers;
using Project_Main.Models.ViewModels.OutputModels;
using Project_Main.Models.ViewModels.TodoListViewModels;
using System.Security.Claims;

namespace Project_Main.Controllers
{
	public class BoardsController : Controller
	{
		private readonly IDataUnitOfWork _dataUnitOfWork;
		private readonly ITodoListRepository _todoListRepository;
		private readonly ILogger<BoardsController> _logger;
		private const int DateCompareValueEarlier = 0;
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
		public async Task<ActionResult<IEnumerable<TodoListModel>>> All()
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(All), controllerName);

			var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var allTodoLists = await _todoListRepository.GetAllWithDetailsAsync(signedInUserId);

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
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

			var todoListFromDb = await _todoListRepository.GetWithDetailsAsync(id);

			if (todoListFromDb is null)
			{
				_logger.LogError(Messages.LogEntityNotFoundInDbSet, operationName, id, HelperDatabase.TodoListsDbSetName);
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

			const int TasksToCompleteCount = 3;
			var tasksComparer = new TasksComparer();

			var tasks = new Task[TasksToCompleteCount]
			{
				Task.Run(() => todoListViewModel.TasksNotCompleted.Sort(tasksComparer)),
				Task.Run(() => todoListViewModel.TasksForToday.Sort(tasksComparer)),
				Task.Run(() => todoListViewModel.TasksCompleted.Sort(tasksComparer)),
			};

			Task.WaitAll(tasks);

			return View(todoListViewModel);
		}
	}
}
