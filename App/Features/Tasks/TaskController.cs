#region USINGS

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using static App.Common.Views.ViewsConsts;
using static App.Common.ControllersConsts;
using App.Features.Tasks.Edit;
using App.Common.ViewModels;
using MediatR;
using App.Features.Tasks.Show;
using App.Features.Tasks.Create.Models;
using App.Features.Tasks.Edit.Models;
using App.Features.Tasks.Delete.Models;
using App.Features.Tasks.Create;
using App.Features.Tasks.Delete;
using App.Common.Helpers;
using App.Common;
using Microsoft.AspNetCore.Identity;
using App.Infrastructure.Databases.Identity.Seeds;
using Azure;
using App.Features.TodoLists.Create;



#endregion

namespace App.Features.Tasks;

/// <summary>
/// Controller to manage Task actions based on specific routes.
/// </summary>
[Authorize]
public class TaskController : Controller
{
	private readonly ILogger<TaskController> _logger;
	private readonly IMediator _mediator;

	/// <summary>
	/// Initializes controller with DbContext and Logger.
	/// </summary>
	/// <param name="context">Database context.</param>
	/// <param name="logger">Logger provider.</param>
	public TaskController(ILogger<TaskController> logger, IMediator mediator)
	{
		_logger = logger;
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

		var response = await _mediator.Send(new ShowTaskQuery(routeTodoListId, routeTaskId));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(response.Data);

		return BadRequest();
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

		var respond = await _mediator.Send(new CreateTaskQuery(id));

		if (respond.StatusCode == StatusCodes.Status200OK)
			return View(respond.Data);

		return BadRequest();
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
	public async Task<IActionResult> Create(int todoListId, TaskCreateInputVM inputVM)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Create), todoListId, nameof(todoListId), _logger);

		var response = await _mediator.Send(new CreateTaskCommand(inputVM, todoListId));

		if (response.StatusCode == StatusCodes.Status201Created)
			return RedirectToAction(TodoListCtrl.ShowAction, TodoListCtrl.Name, response.Data);

        if (response.StatusCode == StatusCodes.Status400BadRequest)
        {
            var responseOnNameTaken = await _mediator.Send(new CreateTaskQuery(inputVM.TodoListId));

            if (responseOnNameTaken.StatusCode == StatusCodes.Status200OK)
            {
                ModelState.AddModelError(string.Empty, response.ErrorMessage!);
                return View(responseOnNameTaken.Data);
            }
        }

        return BadRequest();
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

		var respond = await _mediator.Send(new EditTaskQuery(todoListId, taskId, signedInUserId));

		if (respond.StatusCode == StatusCodes.Status200OK)
			return View(TaskViews.Edit, respond.Data);

		return BadRequest();
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
	public async Task<IActionResult> EditPost([FromForm] TaskEditInputVM inputVM)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(EditPost), inputVM.TodoListId, nameof(inputVM.TodoListId), _logger);
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(EditPost), inputVM.Id, nameof(inputVM.Id), _logger);

		var response = await _mediator.Send(new EditTaskCommand(inputVM));

		if (response.StatusCode == StatusCodes.Status201Created)
			return RedirectToAction(TodoListCtrl.ShowAction, TodoListCtrl.Name, response.Data);

        if (response.StatusCode == StatusCodes.Status400BadRequest)
            return await TryAgainWhenNameTakenAsync(inputVM.TodoListId, inputVM.Id, inputVM.UserId, response.ErrorMessage!);

        return BadRequest();
	}

    private async Task<IActionResult> TryAgainWhenNameTakenAsync(int todoListId, int taskId, string userId, string errorMessage)
    {
        var responseOnNameTaken = await _mediator.Send(new EditTaskQuery(todoListId, taskId, userId));

        if (responseOnNameTaken.StatusCode == StatusCodes.Status200OK)
        {
            ModelState.AddModelError(string.Empty, errorMessage);
            return View(responseOnNameTaken.Data);
        }

        return BadRequest();
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

		var response = await _mediator.Send(new DeleteTaskQuery(todoListId, taskId));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(TaskViews.Delete, response.Data);

		return BadRequest();
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

		var response = await _mediator.Send(new DeleteTaskCommand(deleteInputVM));

		if (response.StatusCode == StatusCodes.Status200OK)
			return RedirectToAction(TodoListCtrl.ShowAction, TodoListCtrl.Name, response.Data);

		return BadRequest();
	}
}
