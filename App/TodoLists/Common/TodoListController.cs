using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Infrastructure.Helpers;
using Web.Infrastructure;
using Web.Databases.Common.Helpers;
using Web.TodoLists.Create;
using Web.TodoLists.Edit;
using Web.Common.ViewModels;
using Web.TodoLists.Common.Interfaces;
using Web.Databases.App.Interfaces;
using Web.Common.Helpers;

namespace Web.TodoLists.Common
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
        private readonly string controllerName = nameof(TodoListController);
        private string operationName = string.Empty;

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
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Edit), controllerName);
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

            var todoListModel = await _todoListRepository.GetAsync(id);

            if (todoListModel == null)
            {
                _logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, id, HelperDatabase.TodoListsDbSetName);
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

            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

            var todoListId = editWrapperVM.OutputVM.Id;

            if (id != todoListId)
            {
                _logger.LogError(MessagesPacket.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, id, todoListId);
                return Conflict();
            }

            var todoListDbModel = await _todoListRepository.GetAsync(todoListId);

            if (todoListDbModel is null)
            {
                _logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, todoListId, HelperDatabase.TodoListsDbSetName);
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
            operationName = HelperOther.CreateActionNameForLoggingAndExceptions(nameof(Delete), controllerName);
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

            //TODO PROBABLY I WOULD NEED TO GET WITH DETAILS TO BE ABLE TO COUNT TASKS ;)
            var todoListDbModel = await _todoListRepository.GetAsync(id);

            if (todoListDbModel == null)
            {
                _logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, id, HelperDatabase.TodoListsDbSetName);
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
            HelperCheck.ThrowExceptionWhenIdLowerThanBottomBoundry(operationName, id, nameof(id), HelperCheck.IdBottomBoundry, _logger);

            if (ModelState.IsValid)
            {
                var todoListDbModel = await _todoListRepository.GetAsync(id);

                if (todoListDbModel != null)
                {
                    if (todoListDbModel.Id != id)
                    {
                        _logger.LogError(MessagesPacket.LogConflictBetweenIdsOfTodoListAndModelObject, operationName, id, todoListDbModel.Id);
                        return Conflict();
                    }

                    _todoListRepository.Remove(todoListDbModel);
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

            var todoListDbModel = await _todoListRepository.GetWithDetailsAsync(id);

            if (todoListDbModel is null)
            {
                _logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, operationName, id, HelperDatabase.TodoListsDbSetName);
                return NotFound();
            }

            var todoListDto = _todoListMapper.TransferToDto(todoListDbModel);
            var detailsOutputVM = _todoListViewModelsFactory.CreateDetailsOutputVM(todoListDto, filterDueDate);

            return View(TodoListViews.Details, detailsOutputVM);
        }
    }
}
