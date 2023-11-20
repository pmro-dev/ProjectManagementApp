using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using static App.Common.Views.ViewsConsts;
using static App.Common.ControllersConsts;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common;
using App.Infrastructure.Databases.App.Interfaces;
using App.Features.Tasks.Edit;
using App.Features.Tasks.Create;
using App.Features.Tasks.Delete;
using App.Infrastructure;
using App.Infrastructure.Helpers;
using App.Common.Helpers;
using App.Common.ViewModels;
using MediatR;
using App.Features.Tasks.Show;

namespace App.Features.Tasks;

/// <summary>
/// Controller to manage Task actions based on specific routes.
/// </summary>
[Authorize]
public class TaskController : Controller
{
	private readonly IDataUnitOfWork _dataUnitOfWork;
	private readonly ITaskRepository _taskRepository;
	private readonly ILogger<TaskController> _logger;
	private readonly ITaskEntityMapper _taskEntityMapper;
	private readonly ITaskViewModelsFactory _taskViewModelsFactory;
	private readonly IMediator _mediator;

	/// <summary>
	/// Initializes controller with DbContext and Logger.
	/// </summary>
	/// <param name="context">Database context.</param>
	/// <param name="logger">Logger provider.</param>
	public TaskController(IDataUnitOfWork dataUnitOfWork, ILogger<TaskController> logger, ITaskEntityMapper taskEntityMapper, ITaskViewModelsFactory taskViewModelsFactory, IMediator mediator)
	{
		_dataUnitOfWork = dataUnitOfWork;
		_taskRepository = _dataUnitOfWork.TaskRepository;
		_logger = logger;
		_taskEntityMapper = taskEntityMapper;
		_taskViewModelsFactory = taskViewModelsFactory;
		_mediator = mediator;
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
	[Route(CustomRoutes.TaskShowRoute)]
	public async Task<IActionResult> Show([FromRoute] int routeTodoListId, [FromRoute] int routeTaskId)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Show), routeTodoListId, nameof(routeTodoListId), _logger);
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Show), routeTaskId, nameof(routeTaskId), _logger);

		var result = await _mediator.Send(new ShowTaskQuery(routeTodoListId, routeTaskId));

		return View(result);
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
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Create), id, nameof(id), _logger);

		if (!ModelState.IsValid)
			return View();

		var result = await _mediator.Send(new CreateTaskQuery(id));

		return View(result);
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
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Create), todoListId, nameof(todoListId), _logger);

		if (!ModelState.IsValid)
			return View(taskCreateWrapperVM);

		var result = await _mediator.Send(new CreateTaskCommand(taskCreateWrapperVM.InputVM, todoListId));

		return RedirectToAction(TodoListCtrl.ShowAction, TodoListCtrl.Name, result);
	}

	/// <summary>
	/// Action GET to edit Task.
	/// </summary>
	/// <param name="todoListId">Target To Do List id for which Task was originally created.</param>
	/// <param name="taskId">Target Task id.</param>
	/// <returns>Return different view based on the final result.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Occurs when one of ids value is invalid.</exception>
	[HttpGet]
	[Route(CustomRoutes.TaskEditGetRoute)]
	public async Task<IActionResult> Edit([FromRoute] int todoListId, [FromRoute] int taskId)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Edit), todoListId, nameof(todoListId), _logger);
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Edit), taskId, nameof(taskId), _logger);

		var signedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		var result = await _mediator.Send(new EditTaskQuery(todoListId, taskId, signedInUserId));

		return View(TaskViews.Edit, result);
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
	[Route(CustomRoutes.TaskEditPostRoute)]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> EditPost([FromForm] WrapperViewModel<TaskEditInputVM, TaskEditOutputVM> editWrapperVM)
	{
		var taskEditInputVM = editWrapperVM.InputVM;

		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(EditPost), taskEditInputVM.TodoListId, nameof(taskEditInputVM.TodoListId), _logger);
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(EditPost), taskEditInputVM.Id, nameof(taskEditInputVM.Id), _logger);

		if (ModelState.IsValid)
		{
			TaskEditInputDto taskEditInputDto = _taskEntityMapper.TransferToDto(taskEditInputVM);
			TaskModel? taskDbModel = await _taskRepository.GetAsync(taskEditInputDto.Id);

			if (taskDbModel is null)
			{
				_logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, nameof(EditPost), nameof(TaskModel), taskEditInputDto.Id);
				return NotFound();
			}

			ExceptionsService.WhenIdsAreNotEqualThrowCritical(nameof(EditPost), taskDbModel.Id, nameof(taskDbModel.Id), taskEditInputDto.Id, nameof(taskEditInputDto.Id), _logger);

			_taskEntityMapper.UpdateModel(taskDbModel, taskEditInputDto);

			_taskRepository.Update(taskDbModel);
			await _dataUnitOfWork.SaveChangesAsync();

			object routeValue = new { id = taskEditInputDto.TodoListId };

			return RedirectToAction(TodoListCtrl.ShowAction, TodoListCtrl.Name, routeValue);
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
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Delete), todoListId, nameof(todoListId), _logger);
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Delete), taskId, nameof(taskId), _logger);

		TaskModel? taskToDeleteModel = await _taskRepository.GetAsync(taskId);

		if (taskToDeleteModel == null)
		{
			_logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, nameof(Delete), nameof(TaskModel), taskId);
			return NotFound();
		}

		TaskDto taskToDeleteDto = _taskEntityMapper.TransferToDto(taskToDeleteModel);

		ExceptionsService.WhenIdsAreNotEqualThrowCritical(nameof(Delete), taskToDeleteDto.TodoListId, nameof(taskToDeleteDto.TodoListId), todoListId, nameof(todoListId), _logger);

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
		TaskDeleteInputVM deleteInputVM = deleteWrapperVM.InputVM;

		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(DeletePost), deleteInputVM.TodoListId, nameof(deleteInputVM.TodoListId), _logger);
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(DeletePost), deleteInputVM.Id, nameof(deleteInputVM.Id), _logger);

		TaskDeleteInputDto deleteInputDto = _taskEntityMapper.TransferToDto(deleteInputVM);

		if (ModelState.IsValid)
		{
			TaskModel? taskToDeleteModel = await _taskRepository.GetAsync(deleteInputDto.Id);

			if (taskToDeleteModel is null)
			{
				_logger.LogError(MessagesPacket.LogEntityNotFoundInDbSet, nameof(DeletePost), nameof(TaskModel), deleteInputDto.Id);
				return NotFound();
			}

			ExceptionsService.WhenIdsAreNotEqualThrowCritical(nameof(DeletePost), taskToDeleteModel.TodoListId, nameof(taskToDeleteModel.TodoListId), deleteInputDto.TodoListId, nameof(deleteInputDto.TodoListId), _logger);

			_taskRepository.Remove(taskToDeleteModel);
			await _dataUnitOfWork.SaveChangesAsync();
		}

		object routeValue = new { id = deleteInputDto.TodoListId };

		return RedirectToAction(TodoListCtrl.ShowAction, TodoListCtrl.Name, routeValue);
	}
}
