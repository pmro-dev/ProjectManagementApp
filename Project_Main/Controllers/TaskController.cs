using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Project_Main.Infrastructure.Helpers;
using static Project_DomainEntities.Helpers.TaskStatusHelper;
using Project_DomainEntities;
using System.Security.Claims;
using Project_Main.Models.DataBases.AppData;
using Project_Main.Models.DataBases.Helpers;
using Microsoft.Data.SqlClient;
using Project_Main.Models.ViewModels.OutputModels;
using Project_DTO;
using Project_Main.Models.ViewModels.InputModels;
using Project_Main.Services;

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
        public async Task<ActionResult<TaskDetailsVM>> Details(int routeTodoListId, int routeTaskId)
        {
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Details), controllerName);

            TaskModel? taskModel = null;

            try
            {
                HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, routeTodoListId, nameof(routeTodoListId), HelperCheck.IdBottomBoundry, _logger);
                HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, routeTaskId, nameof(routeTaskId), HelperCheck.IdBottomBoundry, _logger);

                taskModel = await _taskRepository.GetAsync(routeTaskId);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (ArgumentNullException)
            {
                return RedirectToAction(AccountCtrl.ErrorAction, AccountCtrl.Name);
            }
            catch (SqlException)
            {
                return RedirectToAction(AccountCtrl.ErrorAction, AccountCtrl.Name);
			}

            if (taskModel is null)
            {
                _logger.LogError(Messages.LogEntityNotFoundInDbSet, operationName, routeTaskId, HelperDatabase.TasksDbSetName);
                return NotFound();
            }

            TaskModelDto taskModelDto = TaskDtoService.TransferToDefaultDto(taskModel);
            TaskDetailsVM taskDetailsVM = TaskDtoService.TransferToTaskDetailsVM(taskModelDto);

            if (routeTodoListId != taskDetailsVM.TodoListId)
            {
                _logger.LogError(Messages.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, routeTodoListId, taskModel.TodoListId);
                return Conflict();
            }

            return View(TaskViews.Details, taskDetailsVM);
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
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

            if (!ModelState.IsValid)
                return View();

            // TODO implement new method that allow to get only concrete data that I would specify by Select expression
            var targetTodoListModel = await _todoListRepository.GetAsync(id);

            if (targetTodoListModel is null)
            {
                _logger.LogError(Messages.LogEntityNotFoundInDbSet, operationName, id, HelperDatabase.TodoListsDbSetName);
                return NotFound();
            }

            var todoListModelDto = TodoListDtoService.TransferToDto(targetTodoListModel);
            var taskCreateOutputVM = TaskDtoService.CreateTaskCreateOutputVM(todoListModelDto.Id, todoListModelDto.UserId);

            return View(taskCreateOutputVM);
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
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, todoListId, nameof(todoListId), HelperCheck.IdBottomBoundry, _logger);

            if (!ModelState.IsValid) 
                return View(taskCreateInputVM);

            // TODO Check why are you assign todoList id from route to dto
            taskCreateInputVM.TodoListId = todoListId;

            var taskCreateInputVMDto = TaskDtoService.TransferToTaskCreateInputVMDto(taskCreateInputVM);

            //// TODO Check why are you assign todoList id from route to dto
            //taskCreateInputVM.TodoListId = todoListId;

            var taskModel = TaskDtoService.TransferToTaskModel(taskCreateInputVMDto);

            await _taskRepository.AddAsync(taskModel);
            await _dataUnitOfWork.SaveChangesAsync();

            object routeValue = new { id = taskCreateInputVM.TodoListId };

			return RedirectToAction(BoardsCtrl.SingleDetailsAction, BoardsCtrl.Name, routeValue);
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
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, todoListId, nameof(todoListId), HelperCheck.IdBottomBoundry, _logger);
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, taskId, nameof(taskId), HelperCheck.IdBottomBoundry, _logger);
            
            var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var taskModel = await _taskRepository.GetAsync(taskId);
            TodoListModel? targetTodoList = await _todoListRepository.GetAsync(todoListId);

            // TODO implement method that allow to get only concrete properties by Select expression, here I need only TodoLists Ids and Names
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

            if (!tempTodoLists.Any())
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

            return View(TaskViews.Edit, taskViewModel);
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
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, todoListId, nameof(todoListId), HelperCheck.IdBottomBoundry, _logger);
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

            if (id != taskModel.Id)
            {
                _logger.LogCritical(Messages.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, id, taskModel.Id);
                return Conflict();
            }

            if (ModelState.IsValid)
            {
                 _taskRepository.Update(taskModel);
                await _dataUnitOfWork.SaveChangesAsync();

                object routeValue = new { id = todoListId };

				return RedirectToAction(BoardsCtrl.SingleDetailsAction, BoardsCtrl.Name, routeValue);
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
        [Route(CustomRoutes.TaskDeleteGetRoute)]
        public async Task<IActionResult> Delete(int todoListId, int taskId)
        {
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Delete), controllerName);
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, todoListId, nameof(todoListId), HelperCheck.IdBottomBoundry, _logger);
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, taskId, nameof(taskId), HelperCheck.IdBottomBoundry, _logger);

            TaskModel? taskToDelete = await _taskRepository.GetAsync(taskId);

            if (taskToDelete == null)
            {
                _logger.LogError(Messages.LogEntityNotFoundInDbSet, operationName, taskId, HelperDatabase.TasksDbSetName);
                return NotFound();
            }

			TaskDetailsVM taskDetailsVM = new()
			{
				Id = taskToDelete.Id,
				TodoListId = taskToDelete.TodoListId,
				UserId = taskToDelete.UserId,
				Title = taskToDelete.Title,
				Status = taskToDelete.Status,
				CreationDate = taskToDelete.CreationDate,
				Description = taskToDelete.Description,
				DueDate = taskToDelete.DueDate,
				LastModificationDate = taskToDelete.LastModificationDate,
				ReminderDate = taskToDelete.ReminderDate
			};

			if (taskDetailsVM.TodoListId != todoListId)
            {
                _logger.LogCritical(Messages.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, todoListId, taskToDelete.TodoListId);
                return Conflict();
            }

            return View(TaskViews.Delete, taskDetailsVM);
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
		[HttpPost]
        [Route(CustomRoutes.TaskDeletePostRoute)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost([FromForm] TaskDeleteVM taskDeleteVM)
		{
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, taskDeleteVM.TodoListId, nameof(taskDeleteVM.TodoListId), HelperCheck.IdBottomBoundry, _logger);
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, taskDeleteVM.Id, nameof(taskDeleteVM.Id), HelperCheck.IdBottomBoundry, _logger);

            if (ModelState.IsValid)
            {
                TaskModel? taskToDelete = await _taskRepository.GetAsync(taskDeleteVM.Id);

                if (taskToDelete is null)
                {
                    _logger.LogError(Messages.LogEntityNotFoundInDbSet, operationName, taskDeleteVM.Id, HelperDatabase.TasksDbSetName);
                    return NotFound();
                }

                if (taskToDelete.TodoListId != taskDeleteVM.TodoListId)
                {
                    _logger.LogError(Messages.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, taskDeleteVM.TodoListId, taskToDelete.TodoListId);
                    return Conflict();
                }

                 _taskRepository.Remove(taskToDelete);
                await _dataUnitOfWork.SaveChangesAsync();
            }

			object routeValue = new { id = taskDeleteVM.TodoListId };

			return RedirectToAction(BoardsCtrl.SingleDetailsAction, BoardsCtrl.Name, routeValue);
        }
    }
}
