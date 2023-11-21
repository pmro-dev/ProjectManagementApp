#region USINGS

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static App.Common.Views.ViewsConsts;
using static App.Common.ControllersConsts;
using App.Features.TodoLists.Common.Interfaces;
using App.Infrastructure.Databases.App.Interfaces;
using App.Features.TodoLists.Edit;
using App.Infrastructure;
using App.Features.TodoLists.Create;
using App.Infrastructure.Helpers;
using App.Common.Helpers;
using App.Common.ViewModels;
using App.Features.TodoLists.Common.Models;
using MediatR;
using App.Features.TodoLists.Show;
using App.Features.TodoLists.Duplicate;
using App.Features.Tasks.Edit;
using App.Features.TodoLists.Delete;
using App.Features.TodoLists.Create.Models;
using App.Features.TodoLists.Edit.Models;

#endregion

namespace App.Features.TodoLists;

/// <summary>
/// Controller to manage To Do List actions based on specific routes.
/// </summary>
[Authorize]
public class TodoListController : Controller
{
	private readonly IMediator _mediator;
	private readonly ILogger<TodoListController> _logger;

	/// <summary>
	/// Initializes controller with DbContext and Logger.
	/// </summary>
	/// <param name="context">Database context.</param>
	/// <param name="logger">Logger provider.</param>
	public TodoListController(ILogger<TodoListController> logger, IMediator mediator)
	{
		_logger = logger;
		_mediator = mediator;
	}

	/// <summary>
	/// Action GET to create To Do List.
	/// </summary>
	/// <returns>Create view.</returns>
	[HttpGet]
	[Route(CustomRoutes.TodoListCreateRoute)]
	public async Task<IActionResult> Create()
	{
		var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		var result = await _mediator.Send(new CreateTodoListQuery(userId));

		return View(TodoListViews.Create, result);
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

		string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
		ExceptionsService.WhenPropertyIsNullOrEmptyThrowCritical(nameof(Create), userId, nameof(userId), _logger);

		var result = await _mediator.Send(new CreateTodoListCommand(createWrapperVM, userId));

		if (!result)
		{
			ModelState.AddModelError(string.Empty, MessagesPacket.NameTaken);
			return View(createWrapperVM);
		}

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
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Edit), id, nameof(id), _logger);

		var result = await _mediator.Send(new EditTodoListQuery(id));

		return View(TodoListViews.Edit, result);
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

		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Edit), id, nameof(id), _logger);
		var result = await _mediator.Send(new EditTodoListCommand(editWrapperVM.OutputVM.Id, editWrapperVM, id));

		if (!result)
		{
			return BadRequest();
		}

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
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Delete), id, nameof(id), _logger);

		var result = await _mediator.Send(new DeleteTodoListQuery(id));

		return View(TodoListViews.Delete, result);
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
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(DeletePost), id, nameof(id), _logger);

		if (!ModelState.IsValid)
		{
			return View(TodoListCtrl.DeleteAction);
		}

		var result = await _mediator.Send(new DeleteTodoListCommand(id));

		if (!result) return BadRequest();
		
		return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);
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
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Duplicate), todoListId, nameof(todoListId), _logger);

		var result = await _mediator.Send(new DuplicateTodoListCommand(todoListId));

		if (result)
			return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);

		return BadRequest();
	}


	/// <summary>
	/// Action GET with custom route to show specific To Do List with details.
	/// </summary>
	/// <param name="id">Target To Do List id.</param>
	/// <returns>Single To Do List with details.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
	[HttpGet]
	[Route(CustomRoutes.TodoListShowRoute)]
	public async Task<IActionResult> Show(int id, DateTime? filterDueDate)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Show), id, nameof(id), _logger);

		var result = await _mediator.Send(new ShowTodoListQuery(id, filterDueDate));

		return View(TodoListViews.Show, result);
	}
}
