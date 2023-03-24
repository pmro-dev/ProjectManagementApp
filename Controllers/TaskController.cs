using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TODO_List_ASPNET_MVC.Models.ViewModels;
using TODO_Domain_Entities;
using static TODO_Domain_Entities.Helpers.TaskStatusHelper;
using TODO_List_ASPNET_MVC.Models.DataBase.Abstraction;
using TODO_List_ASPNET_MVC.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TODO_List_ASPNET_MVC.Controllers
{
    /// <summary>
    /// Controller to manage Task actions based on certain routes.
    /// </summary>
    [Authorize]
    public class TaskController : Controller
    {
        private readonly string controllerName = nameof(TaskController);
        private readonly IContextOperations _context;
		private readonly ILogger<TaskController> _logger;
		private readonly SignInManager<IdentityUser> _signInManager;
		private string operationName = string.Empty;

        /// <summary>
        /// Initializes controller with DbContext and Logger.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="logger">Logger provider.</param>
		public TaskController(IContextOperations context, SignInManager<IdentityUser> signInManager, ILogger<TaskController> logger)
        {
            _context = context;
			_logger = logger;
			_signInManager = signInManager;
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
            operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(Details), controllerName);

			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), OtherHelp.idBoundryBottom, _logger);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, taskId, nameof(taskId), OtherHelp.idBoundryBottom, _logger);

			var signedInUserId = _signInManager.UserManager.GetUserId(User);
			var taskModel = await _context.ReadTaskAsync(taskId, signedInUserId);

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

			var todoList = await _context.GetTodoListAsync(todoListId, signedInUserId);

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
            operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(Create), controllerName);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, id, nameof(id), OtherHelp.idBoundryBottom, _logger);

            if (ModelState.IsValid)
            {
				var signedInUserId = _signInManager.UserManager.GetUserId(User);
				var targetTodoList = await _context.GetTodoListAsync(id, signedInUserId);

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
        public async Task<IActionResult> Create(int todoListId, [Bind("Title,Description,DueDate,UserId")] TaskModel taskModel)
        {
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), OtherHelp.idBoundryBottom, _logger);

            if (ModelState.IsValid)
            {
                taskModel.TodoListId = todoListId;
                await _context.CreateTaskAsync(taskModel);
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
            operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(Edit), controllerName);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), OtherHelp.idBoundryBottom, _logger);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, taskId, nameof(taskId), OtherHelp.idBoundryBottom, _logger);
			var signedInUserId = _signInManager.UserManager.GetUserId(User);

			var taskModel = await _context.ReadTaskAsync(taskId, signedInUserId);
            var tempTodoLists = await _context.GetAllTodoListsAsync(signedInUserId);
            var targetTodoList = tempTodoLists.Find(x => x.Id == todoListId);

            if (taskModel == null)
            {
				_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(taskModel));
                return NotFound();
            }

            if (tempTodoLists.Count == OtherHelp.ZeroValueToIndicatesEmptyArray)
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
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), OtherHelp.idBoundryBottom, _logger);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, id, nameof(id), OtherHelp.idBoundryBottom, _logger);

            if (id != taskModel.Id)
            {
				_logger.LogError(Messages.ConflictBetweenTodoListIdsAsParamAndFromModelObjectLogger, operationName, id, taskModel.Id);
                return Conflict();
            }

			if (ModelState.IsValid)
            {
                await _context.UpdateTaskAsync(taskModel);
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
            operationName = OtherHelp.GetActionNameForLoggingAndExceptions(nameof(Delete), controllerName);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), OtherHelp.idBoundryBottom, _logger);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, taskId, nameof(taskId), OtherHelp.idBoundryBottom, _logger);

			var signedInUserId = _signInManager.UserManager.GetUserId(User);
			var taskToDelete = await _context.ReadTaskAsync(taskId, signedInUserId);

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
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, todoListId, nameof(todoListId), OtherHelp.idBoundryBottom, _logger);
			HelperCheck.CheckIdWhenLowerThanBottomBoundryThrowException(operationName, taskId, nameof(taskId), OtherHelp.idBoundryBottom, _logger);

            if (ModelState.IsValid)
            {
				var signedInUserId = _signInManager.UserManager.GetUserId(User);
				var taskToDelete = await _context.ReadTaskAsync(taskId, signedInUserId);

                if (taskToDelete != null)
                {
                    if (taskToDelete.TodoListId != todoListId)
                    {
						_logger.LogError(Messages.ConflictBetweenTodoListIdsAsParamAndFromModelObjectLogger, operationName, todoListId, taskToDelete.TodoListId);
                        return Conflict();
                    }

                    await _context.DeleteTaskAsync(taskToDelete.Id, signedInUserId);
                    return RedirectToAction(nameof(TodoListController.SingleDetails), TodoListController.ShortName, new { id = todoListId });
                }

				_logger.LogError(Messages.EntityNotFoundInDbLogger, operationName, nameof(taskToDelete));
                return NotFound();
            }
            
            return RedirectToAction(nameof(TodoListController.SingleDetails), TodoListController.ShortName, new { id = todoListId });
        }
    }
}
