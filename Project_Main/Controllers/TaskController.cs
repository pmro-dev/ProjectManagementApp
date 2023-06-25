using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Project_Main.Infrastructure.Helpers;
using static Project_DomainEntities.Helpers.TaskStatusHelper;
using Project_DomainEntities;
using Project_Main.Models.ViewModels.TaskViewModels;
using System.Security.Claims;
using Project_Main.Models.DataBases.AppData;
using Project_Main.Models.DataBases.Helpers;
using Project_Main.Models.ViewModels.OutputModels;
using Project_Main.Models.ViewModels.InputModels;

namespace Project_Main.Controllers
{
    /// <summary>
    /// Controller to manage Task actions based on specific routes.
    /// </summary>
    [Authorize]
	public class TaskController : Controller
	{
		private readonly string controllerName = nameof(TaskController);
		private readonly IDataUnitOfWork _dataUnitOfWork;
		private readonly ITaskRepository _taskRepository;
		private readonly ITodoListRepository _todoListRepository;
		private readonly ILogger<TaskController> _logger;
		private string operationName = string.Empty;
		private const string taskDataToBind = "Title,Description,DueDate,ReminderDate,UserId";

		/// <summary>
		/// Initializes controller with DbContext and Logger.
		/// </summary>
		/// <param name="context">Database context.</param>
		/// <param name="logger">Logger provider.</param>
		public TaskController(IDataUnitOfWork dataUnitOfWork, ILogger<TaskController> logger)
		{
			_dataUnitOfWork = dataUnitOfWork;
			_taskRepository = _dataUnitOfWork.TaskRepository;
			_todoListRepository = _dataUnitOfWork.TodoListRepository;
			_logger = logger;
		}

		/// <summary>
		/// Action GET with custom route to show specific To Do List with details ex. Tasks.
		/// </summary>
		/// <param name="routeTodoListId">Target To Do List id.</param>
		/// <param name="routeTaskId">Target Task id.</param>
		/// <returns>
		/// Return different view based on the final result.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when one of ids value is invalid.</exception>
		[HttpGet]
		[Route(CustomRoutes.TaskDetailsRoute)]
		public async Task<ActionResult<TaskModel>> Details(int routeTodoListId, int routeTaskId)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Details), controllerName);

			TaskModel? taskModel = null;

			try
			{
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, routeTodoListId, nameof(routeTodoListId), HelperCheck.BottomBoundryOfId, _logger);
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, routeTaskId, nameof(routeTaskId), HelperCheck.BottomBoundryOfId, _logger);

				taskModel = await _taskRepository.GetAsync(routeTaskId);
			}
			catch (ArgumentOutOfRangeException)
			{
				return NotFound();
			}
			catch (ArgumentNullException)
			{
				return RedirectToAction("Error", "Home");
			}
			catch (SqlException)
			{
				return RedirectToAction("Error", "Home");
			}

			if (taskModel is null)
			{
				_logger.LogError(Messages.LogEntityNotFoundInDbSet, operationName, routeTaskId, HelperDatabase.TasksDbSetName);
				return NotFound();
			}

			if (routeTodoListId != taskModel.TodoListId)
			{
				_logger.LogError(Messages.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, routeTodoListId, taskModel.TodoListId);
				return Conflict();
			}

			return View(taskModel);
		}

		/// <summary>
		/// Action GET to create Task.
		/// </summary>
		/// <param name="id">Target To Do List id for which Task would be created.</param>
		/// <returns>
		/// Return different view based on the final result.
		/// Return: BadRequest when id is invalid, Not Found when there isn't To Do List with given id in Db or return View Create.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
		[HttpGet]
		[Route(CustomRoutes.CreateTaskRoute)]
		public async Task<IActionResult> Create(int id)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Create), controllerName);
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.BottomBoundryOfId, _logger);

			if (ModelState.IsValid is false)
				return View();

				var targetTodoList = await _todoListRepository.GetAsync(id);

				if (targetTodoList is null)
				{
					_logger.LogError(Messages.LogEntityNotFoundInDbSet, operationName, id, HelperDatabase.TodoListsDbSetName);
					return NotFound();
				}

				var taskViewModel = new TaskViewModel()
				{
					TodoListName = targetTodoList.Title,
					TodoListId = targetTodoList.Id,
					UserId = targetTodoList.UserId,
					ReminderDate = null
				};

				return View(taskViewModel);
			}

		/// <summary>
		/// Action POST to create Task.
		/// </summary>
		/// <param name="todoListId">Targeted To Do List for which Task would be created.</param>
        /// <param name="taskCreateInputVM">Task Model with data that comes from form.</param>
		/// <returns>
		/// Return different view based on the final result.
		/// Return: BadRequest when id is invalid or redirect to view with target To Do List SingleDetails.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
		[HttpPost]
		[Route(CustomRoutes.CreateTaskPostRoute)]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int todoListId, [Bind(taskDataToBind)] TaskCreateInputVM taskCreateInputVM)
		{
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, todoListId, nameof(todoListId), HelperCheck.BottomBoundryOfId, _logger);
            if (ModelState.IsValid is false) 
                return View(taskCreateInputVM);

            // TODO Check why are you assign todoList id from route to dto
            taskCreateInputVM.TodoListId = todoListId;
				TaskDataFromForm.TodoListId = todoListId;
				await _taskRepository.AddAsync(TaskDataFromForm);
            await _taskRepository.AddAsync(taskModel);
            await _taskRepository.AddAsync(taskCreateInputVM);
				await _dataUnitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(TodoListController.SingleDetails), TodoListController.ShortName, new { id = taskCreateInputVM.TodoListId });
		}

		/// <summary>
		/// Action GET to edit Task.
		/// </summary>
		/// <param name="todoListId">Target To Do List id for which Task was originally created.</param>
		/// <param name="taskId">Target Task id.</param>
		/// <returns>Return different view based on the final result.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when one of ids value is invalid.</exception>
		[HttpGet]
		[Route(CustomRoutes.TaskEditRoute)]
		public async Task<IActionResult> Edit(int todoListId, int taskId)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Edit), controllerName);
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, todoListId, nameof(todoListId), HelperCheck.BottomBoundryOfId, _logger);
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, taskId, nameof(taskId), HelperCheck.BottomBoundryOfId, _logger);
			var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var taskModel = await _taskRepository.GetAsync(taskId);
			TodoListModel? targetTodoList = await _todoListRepository.GetAsync(todoListId);
			IEnumerable<TodoListModel> tempTodoLists = await _todoListRepository.GetAllByFilterAsync(todoList => todoList.UserId == signedInUserId);

			if (taskModel == null)
			{
				_logger.LogError(Messages.LogEntityNotFoundInDbSet, operationName, taskId, HelperDatabase.TasksDbSetName);
				return NotFound();
			}

			if (targetTodoList == null)
			{
				_logger.LogError(Messages.LogEntityNotFoundInDbSet, operationName, todoListId, HelperDatabase.TodoListsDbSetName);
				return NotFound();
			}

			if (tempTodoLists.Any() is false)
			{
				_logger.LogInformation(Messages.LogNotAnyTodoListInDb, operationName);
				return NotFound();
			}

			if (taskModel.TodoListId != targetTodoList.Id)
			{
				_logger.LogError(Messages.LogConflictBetweenTodoListIdsFromTodoListModelAndTaskModel, operationName, taskModel.TodoListId, targetTodoList.Id);
				return Conflict();
			}

			string dataValueField = "Value";
			string dataTextField = "Text";

			var todoListsSelectorData = new SelectList(tempTodoLists, nameof(TodoListModel.Id), nameof(TodoListModel.Title), todoListId);
			var statusesSelectorData = new SelectList(Enum.GetValues(typeof(TaskStatusType)).Cast<TaskStatusType>().Select(v => new SelectListItem
			{
				Text = v.ToString(),
				Value = ((int)v).ToString()
			}).ToList(), dataValueField, dataTextField, taskModel.Status);

            var taskViewModel = new TaskCreateInputVM
			{
				Title = taskModel.Title,
				Description = taskModel.Description,
				DueDate = taskModel.DueDate,
				CreationDate = taskModel.CreationDate,
				LastModificationDate = taskModel.LastModificationDate,
				ReminderDate = taskModel.ReminderDate,
				Id = taskModel.Id,
				Status = taskModel.Status,
				StatusSelector = statusesSelectorData,
				TodoListId = taskModel.TodoListId,
				TodoListName = targetTodoList.Title,
				UserId = taskModel.UserId,
				TodoListsSelector = todoListsSelectorData
			};

			return View(taskViewModel);
		}

		/// <summary>
		/// Action POST to EDIT Task.
		/// </summary>
		/// <param name="todoListId">Target To Do List for which Task is assigned.</param>
		/// <param name="id">Target Task id.</param>
		/// <param name="taskModel">Model with form's data.</param>
		/// <returns>Return different respond based on the final result.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when one of ids value is invalid.</exception>
		[HttpPost]
		[Route(CustomRoutes.TaskEditRoute)]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int todoListId, int id, TaskModel taskModel)
		{
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, todoListId, nameof(todoListId), HelperCheck.BottomBoundryOfId, _logger);
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.BottomBoundryOfId, _logger);

			if (id != taskModel.Id)
			{
				_logger.LogCritical(Messages.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, id, taskModel.Id);
				return Conflict();
			}

			if (ModelState.IsValid)
			{
				await _taskRepository.Update(taskModel);
				await _dataUnitOfWork.SaveChangesAsync();

				return RedirectToAction(nameof(TodoListController.SingleDetails), TodoListController.ShortName, new { id = todoListId });
			}

			return View(taskModel);
		}

		/// <summary>
		/// Action GET to DELETE To Do List.
		/// </summary>
		/// <param name="todoListId">Target To Do List id for which Task was assigned.</param>
		/// <param name="taskId">Target Task id.</param>
		/// <returns>Return different respond / view based on the final result. 
		/// Return Bad Request when given To Do List id is not equal to To Do List Id in Task property, 
		/// Not Found when there isn't such Task in Db or return view to further operations.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when one of ids value is invalid.</exception>
		[HttpGet]
		[Route(CustomRoutes.TaskDeleteRoute, Name = "deletetask")]
		public async Task<IActionResult> Delete(int todoListId, int taskId)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Delete), controllerName);
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, todoListId, nameof(todoListId), HelperCheck.BottomBoundryOfId, _logger);
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, taskId, nameof(taskId), HelperCheck.BottomBoundryOfId, _logger);

			TaskModel? taskToDelete = await _taskRepository.GetAsync(taskId);

			if (taskToDelete == null)
			{
				_logger.LogError(Messages.LogEntityNotFoundInDbSet, operationName, taskId, HelperDatabase.TasksDbSetName);
				return NotFound();
			}

			if (taskToDelete.TodoListId != todoListId)
			{
				_logger.LogCritical(Messages.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, todoListId, taskToDelete.TodoListId);
				return Conflict();
			}

			return View(taskToDelete);
		}

		/// <summary>
		/// Action POST to DELETE Task.
		/// </summary>
		/// <param name="todoListId">Target To Do List id for which Task was assigned.</param>
		/// <param name="taskId">Target Task id.</param>
		/// <returns>
		/// Return different view based on the final result. 
		/// Return Bad Request when one of the given id is out of range, 
		/// Not Found when there isn't such Task in Db or redirect to view with To Do List details.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when one of ids value is invalid.</exception>
		[HttpPost, ActionName("Delete")]
		[Route(CustomRoutes.TaskDeleteRoute, Name = "deleteTaskInvoke")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int todoListId, int taskId)
		{
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, todoListId, nameof(todoListId), HelperCheck.BottomBoundryOfId, _logger);
			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, taskId, nameof(taskId), HelperCheck.BottomBoundryOfId, _logger);

			if (ModelState.IsValid)
			{
				TaskModel? taskToDelete = await _taskRepository.GetAsync(taskId);

				if (taskToDelete is null)
				{
					_logger.LogError(Messages.LogEntityNotFoundInDbSet, operationName, taskId, HelperDatabase.TasksDbSetName);
					return NotFound();
				}

				if (taskToDelete.TodoListId != todoListId)
				{
					_logger.LogError(Messages.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, todoListId, taskToDelete.TodoListId);
					return Conflict();
				}

				await _taskRepository.Remove(taskToDelete);
				await _dataUnitOfWork.SaveChangesAsync();

				return RedirectToAction(nameof(TodoListController.SingleDetails), TodoListController.ShortName, new { id = todoListId });
			}

			return RedirectToAction(nameof(TodoListController.SingleDetails), TodoListController.ShortName, new { id = todoListId });
		}
	}
}
