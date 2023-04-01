using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Project_Main.Infrastructure.Helpers;
using static Project_DomainEntities.Helpers.TaskStatusHelper;
using Project_DomainEntities;
using Project_Main.Models.ViewModels.TaskViewModels;
using System.Security.Claims;
using Project_Main.Models.DataBases.Repositories.AppData;

namespace Project_Main.Controllers
{
    /// <summary>
    /// Controller to manage Task actions based on certain routes.
    /// </summary>
    [Authorize]
	public class TaskController : Controller
	{
		private readonly string controllerName = nameof(TaskController);
		private readonly IDataUnitOfWork _dataUnitOfWork;
		private readonly ILogger<TaskController> _logger;
		private string operationName = string.Empty;

		/// <summary>
		/// Initializes controller with DbContext and Logger.
		/// </summary>
		/// <param name="context">Database context.</param>
		/// <param name="logger">Logger provider.</param>
		public TaskController(IDataUnitOfWork dataUnitOfWork, ILogger<TaskController> logger)
		{
			_dataUnitOfWork = dataUnitOfWork;
			_logger = logger;
		}

		/// <summary>
		/// Action GET with custom route to show specific To Do List with details.
		/// </summary>
		/// <param name="todoListId">Target To Do List id.</param>
		/// <param name="taskId">Target Task id.</param>
		/// <returns>
		/// Return different view based on the final result.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when one of ids value is invalid.</exception>
		[HttpGet]
		[Route("TodoList/{todoListId:int}/[controller]/{taskId:int}/Details")]
		public async Task<ActionResult<TaskModel>> Details(int todoListId, int taskId)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Details), controllerName);

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), HelperOther.idBoundryBottom, _logger);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, taskId, nameof(taskId), HelperOther.idBoundryBottom, _logger);

			ITaskRepository taskRepository = _dataUnitOfWork.TaskRepository;
			TaskModel? taskModel = await taskRepository.GetAsync(taskId);

			if (taskModel == null)
			{
				_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(taskModel));
				return NotFound();
			}

			if (todoListId != taskModel.TodoListId)
			{
				_logger.LogError(Messages.ConflictBetweenTodoListIdsAsParamAndFromModelObjectLogger, operationName, todoListId, taskModel.TodoListId);
				return Conflict();
			}

			ITodoListRepository todoListRepository = _dataUnitOfWork.TodoListRepository;

			TodoListModel? todoList = await todoListRepository.GetAsync(todoListId);

			if (todoList == null)
			{
				_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(todoList));
				return NotFound();
			}

			if (taskModel.TodoListId != todoList.Id)
			{
				_logger.LogError(Messages.ConflictBetweenTodoListIdsFromTodoListModelAndTaskModelLogger, operationName, taskModel.TodoListId, todoList.Id);
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
		/// Return: BadRequest when id is invalid, Not Found when there isn't To Do List with given id in Db or View Create.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
		[HttpGet]
		[Route("TodoList/{id:int}/Create")]
		public async Task<IActionResult> Create(int id)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Create), controllerName);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, id, nameof(id), HelperOther.idBoundryBottom, _logger);

			if (ModelState.IsValid)
			{
				ITodoListRepository todoListRepository = _dataUnitOfWork.TodoListRepository;
				var targetTodoList = await todoListRepository.GetAsync(id);

				if (targetTodoList is null)
				{
					_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(targetTodoList));
					return NotFound();
				}

				var taskViewModel = new TaskViewModel()
				{
					TodoListName = targetTodoList.Name,
					TodoListId = targetTodoList.Id,
					UserId = targetTodoList.UserId,
					ReminderDate = null
				};

				return View(taskViewModel);
			}

			return View();
		}

		/// <summary>
		/// Action POST to create Task.
		/// </summary>
		/// <param name="todoListId">Target To Do List for which Task would be created.</param>
		/// <param name="taskModel">Model with form's data.</param>
		/// <returns>
		/// Return different view based on the final result.
		/// Return: BadRequest when id is invalid or redirect to view with target To Do List SingleDetails.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
		[HttpPost]
		[Route("TodoList/{todoListId:int}/Create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(int todoListId, [Bind("Title,Description,DueDate,ReminderDate,UserId")] TaskModel taskModel)
		{
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), HelperOther.idBoundryBottom, _logger);

			if (ModelState.IsValid)
			{
				taskModel.TodoListId = todoListId;
				ITaskRepository taskRepository = _dataUnitOfWork.TaskRepository;
				await taskRepository.AddAsync(taskModel);
				await _dataUnitOfWork.SaveChangesAsync();

				return RedirectToAction(nameof(TodoListController.SingleDetails), TodoListController.ShortName, new { id = todoListId });
			}

			return View(taskModel);
		}

		/// <summary>
		/// Action GET to edit Task.
		/// </summary>
		/// <param name="todoListId">Target To Do List id for which Task was originally created.</param>
		/// <param name="taskId">Target Task id.</param>
		/// <returns>Return different view based on the final result.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Occurs when one of ids value is invalid.</exception>
		[HttpGet]
		[Route("TodoList/{todoListId:int}/[controller]/{taskId:int}/[action]")]
		public async Task<IActionResult> Edit(int todoListId, int taskId)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Edit), controllerName);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), HelperOther.idBoundryBottom, _logger);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, taskId, nameof(taskId), HelperOther.idBoundryBottom, _logger);
			var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			ITaskRepository taskRepository = _dataUnitOfWork.TaskRepository;
			var taskModel = await taskRepository.GetAsync(taskId);

			ITodoListRepository todoListRepository = _dataUnitOfWork.TodoListRepository;
			TodoListModel? targetTodoList = await todoListRepository.GetAsync(todoListId);
			IEnumerable<TodoListModel> tempTodoLists = await todoListRepository.GetByFilterAsync(todoList => todoList.UserId == signedInUserId);

			if (taskModel == null)
			{
				_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(taskModel));
				return NotFound();
			}

			if (tempTodoLists.Any())
			{
				_logger.LogInformation(Messages.NotAnyTodoListInDb, operationName);
				return NotFound();
			}

			if (targetTodoList == null)
			{
				_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(targetTodoList));
				return NotFound();
			}

			if (taskModel.TodoListId != targetTodoList.Id)
			{
				_logger.LogError(Messages.ConflictBetweenTodoListIdsFromTodoListModelAndTaskModelLogger, operationName, taskModel.TodoListId, targetTodoList.Id);
				return Conflict();
			}

			var todoListsSelectorData = new SelectList(tempTodoLists, nameof(TodoListModel.Id), nameof(TodoListModel.Name), todoListId);
			var statusesSelectorData = new SelectList(Enum.GetValues(typeof(TaskStatusType)).Cast<TaskStatusType>().Select(v => new SelectListItem
			{
				Text = v.ToString(),
				Value = ((int)v).ToString()
			}).ToList(), "Value", "Text", taskModel.Status);

			var taskViewModel = new TaskViewModel
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
				TodoListName = targetTodoList.Name,
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
		[Route("TodoList/{todoListId:int}/[controller]/{taskId:int}/[action]")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int todoListId, int id, TaskModel taskModel)
		{
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), HelperOther.idBoundryBottom, _logger);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, id, nameof(id), HelperOther.idBoundryBottom, _logger);

			if (id != taskModel.Id)
			{
				_logger.LogError(Messages.ConflictBetweenTodoListIdsAsParamAndFromModelObjectLogger, operationName, id, taskModel.Id);
				return Conflict();
			}

			if (ModelState.IsValid)
			{
				ITaskRepository taskRepository = _dataUnitOfWork.TaskRepository;
				taskRepository.Update(taskModel);
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
		[Route("TodoList/{todoListId:int}/Task/{taskId:int}/[action]", Name = "deletetask")]
		public async Task<IActionResult> Delete(int todoListId, int taskId)
		{
			operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Delete), controllerName);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), HelperOther.idBoundryBottom, _logger);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, taskId, nameof(taskId), HelperOther.idBoundryBottom, _logger);

			ITaskRepository taskRepository = _dataUnitOfWork.TaskRepository;
			TaskModel? taskToDelete = await taskRepository.GetAsync(taskId);

			if (taskToDelete == null)
			{
				_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(taskToDelete));
				return NotFound();
			}

			if (taskToDelete.TodoListId != todoListId)
			{
				_logger.LogError(Messages.ConflictBetweenTodoListIdsAsParamAndFromModelObjectLogger, operationName, todoListId, taskToDelete.TodoListId);
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
		[Route("TodoList/{todoListId:int}/Task/{taskId:int}/[action]", Name = "deleteTaskInvoke")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int todoListId, int taskId)
		{
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), HelperOther.idBoundryBottom, _logger);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, taskId, nameof(taskId), HelperOther.idBoundryBottom, _logger);

			if (ModelState.IsValid)
			{
				ITaskRepository taskRepository = _dataUnitOfWork.TaskRepository;
				TaskModel? taskToDelete = await taskRepository.GetAsync(taskId);
				
				if (taskToDelete != null)
				{
					if (taskToDelete.TodoListId != todoListId)
					{
						_logger.LogError(Messages.ConflictBetweenTodoListIdsAsParamAndFromModelObjectLogger, operationName, todoListId, taskToDelete.TodoListId);
						return Conflict();
					}

					taskRepository.Remove(taskToDelete);
					await _dataUnitOfWork.SaveChangesAsync();

					return RedirectToAction(nameof(TodoListController.SingleDetails), TodoListController.ShortName, new { id = todoListId });
				}

				_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(taskToDelete));
				return NotFound();
			}

			return RedirectToAction(nameof(TodoListController.SingleDetails), TodoListController.ShortName, new { id = todoListId });
		}
	}
}
