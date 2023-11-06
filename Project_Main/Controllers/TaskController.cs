using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Project_Main.Infrastructure.Helpers;
using static Project_DomainEntities.Helpers.TaskStatusHelper;
using Project_DomainEntities;
using System.Security.Claims;
using Project_Main.Models.DataBases.AppData;
using Project_Main.Models.DataBases.Helpers;
using Project_Main.Services.DTO;
using Project_Main.Models.Inputs.DTOs;
using Project_Main.Models.DTOs;
using Project_Main.Models.Inputs.ViewModels;
using Project_Main.Models.Outputs.ViewModels;
using Project_Main.Models.Factories.ViewModels;
using Project_Main.Models.Generics.ViewModels.WrapperModels;

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
        private readonly ITaskEntityMapper _taskEntityMapper;
        private readonly ITodoListMapper _todoListMapper;
        private readonly ITaskViewModelsFactory _taskViewModelsFactory;

        private string operationName = string.Empty;

        /// <summary>
        /// Initializes controller with DbContext and Logger.
        /// </summary>
        /// <param name="context">Database context.</param>
        /// <param name="logger">Logger provider.</param>
        public TaskController(IDataUnitOfWork dataUnitOfWork, ILogger<TaskController> logger, ITaskEntityMapper taskEntityMapper, ITodoListMapper todoListMapper, ITaskViewModelsFactory taskViewModelsFactory)
        {
            _dataUnitOfWork = dataUnitOfWork;
            _taskRepository = _dataUnitOfWork.TaskRepository;
            _todoListRepository = _dataUnitOfWork.TodoListRepository;
            _logger = logger;
            _taskEntityMapper = taskEntityMapper;
            _todoListMapper = todoListMapper;
            _taskViewModelsFactory = taskViewModelsFactory;
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
        public async Task<IActionResult> Details(int routeTodoListId, int routeTaskId)
        {
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Details), controllerName);

            //TODO find and implement the right approach of handling exceptions and params validations
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, routeTodoListId, nameof(routeTodoListId), HelperCheck.IdBottomBoundry, _logger);
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, routeTaskId, nameof(routeTaskId), HelperCheck.IdBottomBoundry, _logger);

            ITaskModel? taskModel = await _taskRepository.GetAsync(routeTaskId);

            if (taskModel is null)
            {
                _logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, routeTaskId, HelperDatabase.TasksDbSetName);
                return NotFound();
            }

            if (routeTodoListId != taskModel.TodoListId)
            {
                _logger.LogError(MessagesPacket.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, routeTodoListId, taskModel.TodoListId);
                return Conflict();
            }

            //TODO THOSE OPERATIONS MOVE TO TASKSERVICE
            ITaskDto taskDto = _taskEntityMapper.TransferToDto(taskModel);
            var detailsOutputVM = _taskViewModelsFactory.CreateDetailsOutputVM(taskDto);
            //END TO DO

            return View(TaskViews.Details, detailsOutputVM);
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

            ITodoListModel? todoListModel = await _todoListRepository.GetAsync(id);

            if (todoListModel is null)
            {
                _logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, id, HelperDatabase.TodoListsDbSetName);
                return NotFound();
            }

            //TODO MOVE THOSE OPERATIONS TO TASKSERVICE
            ITodoListDto todoListDto = _todoListMapper.TransferToDto(todoListModel);
            var taskCreateOutputVM = _taskViewModelsFactory.CreateCreateOutputVM(todoListDto);
            var taskCreateWrapperVM = _taskViewModelsFactory.CreateWrapperCreateVM();
            taskCreateWrapperVM.OutputVM = taskCreateOutputVM;
            //END TO DO

            return View(taskCreateWrapperVM);
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
        public async Task<IActionResult> Create(int todoListId, WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM> taskCreateWrapperVM)
        {
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, todoListId, nameof(todoListId), HelperCheck.IdBottomBoundry, _logger);

            if (!ModelState.IsValid)
                return View(taskCreateWrapperVM);

            ITaskCreateInputVM inputVM = taskCreateWrapperVM.InputVM;
            ITaskDto taskDto = _taskEntityMapper.TransferToDto(inputVM);
            ITaskModel taskModel = _taskEntityMapper.TransferToModel(taskDto);

            await _taskRepository.AddAsync((TaskModel)taskModel);
            await _dataUnitOfWork.SaveChangesAsync();

            object routeValue = new { id = taskDto.TodoListId };

            return RedirectToAction(TodoListCtrl.DetailsAction, TodoListCtrl.Name, routeValue);
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

            ITaskModel? taskModel = await _taskRepository.GetAsync(taskId);

            if (taskModel == null)
            {
                _logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, taskId, HelperDatabase.TasksDbSetName);
                return NotFound();
            }

            ITodoListModel? targetTodoListModel = await _todoListRepository.GetAsync(todoListId);

            if (targetTodoListModel == null)
            {
                _logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, todoListId, HelperDatabase.TodoListsDbSetName);
                return NotFound();
            }

            if (taskModel.TodoListId != targetTodoListModel.Id)
            {
                _logger.LogError(MessagesPacket.LogConflictBetweenTodoListIdsFromTodoListModelAndTaskModel, operationName, taskModel.TodoListId, targetTodoListModel.Id);
                return Conflict();
            }

            ITaskDto taskDto = _taskEntityMapper.TransferToDto(taskModel);

			// TODO implement method that allow to get only concrete properties by Select expression, here I need only TodoLists Ids and Names
			var userTodoListModels = await _todoListRepository.GetAllByFilterAsync(todoList => todoList.UserId == signedInUserId);

			if (!userTodoListModels.Any())
            {
                _logger.LogInformation(MessagesPacket.LogNotAnyTodoListInDb, operationName);
                return NotFound();
            }

            var castedListModels = userTodoListModels.Cast<ITodoListModel>().ToList();

			ICollection<ITodoListDto> userTodoListDtos = _todoListMapper.TransferToDto(castedListModels);
            // END OF TO DO

            string dataValueField = "Value";
            string dataTextField = "Text";

            var todoListSelectorDto = new SelectList(userTodoListDtos, nameof(ITodoListDto.Id), nameof(ITodoListDto.Title), todoListId);
            var taskStatusSelectorDto = new SelectList(Enum.GetValues(typeof(TaskStatusType))
                .Cast<TaskStatusType>()
                .Select(taskStatusType => new SelectListItem
                {
                    Text = taskStatusType.ToString(),
                    Value = ((int)taskStatusType).ToString()
                })
                .ToList(),
                dataValueField,
                dataTextField,
                taskDto.Status);

            var editOutputVM = _taskViewModelsFactory.CreateEditOutputVM(taskDto, taskStatusSelectorDto, todoListSelectorDto);
            var editWrapperVM = _taskViewModelsFactory.CreateWrapperEditVM();
			editWrapperVM.OutputVM = editOutputVM;

			return View(TaskViews.Edit, editWrapperVM);
        }

        /// <summary>
        /// Action POST to EDIT Task.
        /// </summary>
        /// <param name="todoListId">Target To Do List for which Task is assigned.</param>
        /// <param name="id">Target Task id.</param>
        /// <param name="taskDto">Model with form's data.</param>
        /// <returns>Return different respond based on the final result.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Occurs when one of ids value is invalid.</exception>
        [HttpPost]
        [Route(CustomRoutes.TaskEditRoute)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int todoListId, int id, WrapperViewModel<TaskEditInputVM, TaskEditOutputVM> editWrapperVM)
        {
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, todoListId, nameof(todoListId), HelperCheck.IdBottomBoundry, _logger);
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

            var taskEditInputVM = editWrapperVM.InputVM;

			if (id != taskEditInputVM.Id)
            {
                _logger.LogCritical(MessagesPacket.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, id, taskEditInputVM.Id);
                return Conflict();
            }

            if (ModelState.IsValid)
            {
                ITaskEditInputDto taskEditInputDto = _taskEntityMapper.TransferToDto(taskEditInputVM);
                ITaskModel? taskDbModel = await _taskRepository.GetAsync(id);

                if (taskDbModel is null)
                {
                    _logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, taskEditInputDto.Id, HelperDatabase.TasksDbSetName);
                    return NotFound();
                }

                if (taskDbModel.Id != id)
                {//TODO WRITE NEW LOG MESSAGE FOR THIS SITUATION
                    _logger.LogCritical(MessagesPacket.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, id, taskDbModel.Id);
                    return Conflict();
                }

                _taskEntityMapper.UpdateModel(taskDbModel, taskEditInputDto);

                _taskRepository.Update((TaskModel)taskDbModel);
                await _dataUnitOfWork.SaveChangesAsync();

                object routeValue = new { id = todoListId };

                return RedirectToAction(TodoListCtrl.DetailsAction, TodoListCtrl.Name, routeValue);
            }

            return View(editWrapperVM);
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

            ITaskModel? taskToDeleteModel = await _taskRepository.GetAsync(taskId);

            if (taskToDeleteModel == null)
            {
                _logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, taskId, HelperDatabase.TasksDbSetName);
                return NotFound();
            }

            ITaskDto taskToDeleteDto = _taskEntityMapper.TransferToDto(taskToDeleteModel);

            if (taskToDeleteDto.TodoListId != todoListId)
            {
                _logger.LogCritical(MessagesPacket.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, todoListId, taskToDeleteDto.TodoListId);
                return Conflict();
            }

            var deleteOutputVM = _taskViewModelsFactory.CreateDeleteOutputVM(taskToDeleteDto);
            var deleteWrapperVM = _taskViewModelsFactory.CreateWrapperDeleteVM();
            deleteWrapperVM.OutputVM = deleteOutputVM;

			return View(TaskViews.Delete, deleteWrapperVM);
        }

        /// <summary>
        /// Action POST to DELETE Task.
        /// </summary>
        /// <param name="deleteInputVM">Targets ViewModel for TaskDelete.</param>
        /// <returns>
        /// Return different view based on the final result. 
        /// Return Bad Request when one of the given id is out of range, 
        /// Not Found when there isn't such Task in Db or redirect to view with To Do List details.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Occurs when one of ids value is invalid.</exception>
        [HttpPost]
        [Route(CustomRoutes.TaskDeletePostRoute)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost([FromForm] WrapperViewModel<TaskDeleteInputVM, TaskDeleteOutputVM> deleteWrapperVM)
        {
            ITaskDeleteInputVM deleteInputVM = deleteWrapperVM.InputVM;

			HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, deleteInputVM.TodoListId, nameof(deleteInputVM.TodoListId), HelperCheck.IdBottomBoundry, _logger);
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, deleteInputVM.Id, nameof(deleteInputVM.Id), HelperCheck.IdBottomBoundry, _logger);
            
            ITaskDeleteInputDto deleteInputDto = _taskEntityMapper.TransferToDto(deleteInputVM);

            if (ModelState.IsValid)
            {
                ITaskModel? taskToDeleteModel = await _taskRepository.GetAsync(deleteInputDto.Id);

                if (taskToDeleteModel is null)
                {
                    _logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, deleteInputDto.Id, HelperDatabase.TasksDbSetName);
                    return NotFound();
                }

                if (taskToDeleteModel.TodoListId != deleteInputDto.TodoListId)
                {
                    _logger.LogError(MessagesPacket.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, deleteInputDto.TodoListId, taskToDeleteModel.TodoListId);
                    return Conflict();
                }

                _taskRepository.Remove((TaskModel)taskToDeleteModel);
                await _dataUnitOfWork.SaveChangesAsync();
            }

            object routeValue = new { id = deleteInputDto.TodoListId };

            return RedirectToAction(TodoListCtrl.DetailsAction, TodoListCtrl.Name, routeValue);
        }
    }
}
