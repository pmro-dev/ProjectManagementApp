﻿#region USINGS

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static App.Common.Views.ViewsConsts;
using static App.Common.ControllersConsts;
using App.Features.TodoLists.Common.Interfaces;
using App.Infrastructure.Databases.App.Interfaces;
using App.Features.TodoLists.Edit;
using App.Features.TodoLists.Create;
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
using App.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Azure;


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

		var response = await _mediator.Send(new CreateTodoListQuery(userId));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(TodoListViews.Create, response.Data);

		return BadRequest();
	}

	/// <summary>
	/// Action POST to create To Do List.
	/// </summary>
	/// <param name="todoListModel">Model with form's data.</param>
	/// <returns>Return different view based on the final response. Redirect to Briefly or to view with form.</returns>
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(TodoListCreateInputVM inputVM)
	{
		var response = await _mediator.Send(new CreateTodoListCommand(inputVM));

		if (response.StatusCode == StatusCodes.Status201Created)
			return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);

		if (response.StatusCode == StatusCodes.Status400BadRequest)
			return await TryAgainWhenNameTakenAsync(inputVM.UserId, response.ErrorMessage!);

		return BadRequest();
	}

	private async Task<IActionResult> TryAgainWhenNameTakenAsync(string userId, string errorMessage)
	{
        var responseOnNameTaken = await _mediator.Send(new CreateTodoListQuery(userId));

        if (responseOnNameTaken.StatusCode == StatusCodes.Status200OK)
        {
            ModelState.AddModelError(string.Empty, errorMessage);
            return View(responseOnNameTaken.Data);
        }

		return BadRequest();
    }

	/// <summary>
	/// Action GET to EDIT To Do List.
	/// </summary>
	/// <param name="id">Target To Do List id.</param>
	/// <returns>Return different view based on the final response. Return Bad Request when given id is invalid, Return Not Found when there isn't such To Do List in Db or return edit view.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
	[HttpGet]
	[Route(CustomRoutes.TodoListEditRoute)]
	public async Task<IActionResult> Edit(int id)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Edit), id, nameof(id), _logger);

		var response = await _mediator.Send(new EditTodoListQuery(id));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(TodoListViews.Edit, response.Data);

		return BadRequest();
	}

	/// <summary>
	/// Action POST to EDIT To Do List.
	/// </summary>
	/// <param name="id">Target To Do List id.</param>
	/// <param name="todoListModel">Model with form's data.</param>
	/// <returns>
	/// Return different view based on the final response. Return Bad Request when given id is invalid or id is not equal to model id, 
	/// Redirect to index view when updating operation succeed.
	/// </returns>
	/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
	[HttpPost]
	[Route(CustomRoutes.TodoListEditRoute)]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, [FromForm] TodoListEditInputVM inputVM)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Edit), id, nameof(id), _logger);
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Edit), inputVM.Id, nameof(inputVM.Id), _logger);

		var response = await _mediator.Send(new EditTodoListCommand(inputVM, id));

		if (response.StatusCode == StatusCodes.Status201Created)
			return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);

		return BadRequest();
	}

	/// <summary>
	/// Action GET to DELETE To Do List.
	/// </summary>
	/// <param name="id">Target To Do List id.</param>
	/// <returns>
	/// Return different view based on the final response. 
	/// Not Found when there isn't such To Do List in Db or return view when delete operation succeed.
	/// </returns>
	/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
	[HttpGet]
	[Route(CustomRoutes.TodoListDeleteRoute)]
	public async Task<IActionResult> Delete(int id)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(Delete), id, nameof(id), _logger);

		var response = await _mediator.Send(new DeleteTodoListQuery(id));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(TodoListViews.Delete, response.Data);

		return BadRequest();
	}

	/// <summary>
	/// Action POST to DELETE To Do List.
	/// </summary>
	/// <param name="id">Target To Do List id.</param>
	/// <returns>
	/// Return different view based on the final response. 
	/// Return Conflict when given id and To Do List id of object from Database are not equal, 
	/// </returns>
	/// <exception cref="ArgumentOutOfRangeException">Occurs when id value is invalid.</exception>
	[HttpPost]
	[Route(CustomRoutes.TodoListDeletePostRoute)]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeletePost(int id)
	{
		ExceptionsService.WhenIdLowerThanBottomBoundryThrowError(nameof(DeletePost), id, nameof(id), _logger);

		var response = await _mediator.Send(new DeleteTodoListCommand(id));

		if (response.StatusCode == StatusCodes.Status200OK)
			return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);

		return BadRequest();
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

		var response = await _mediator.Send(new DuplicateTodoListCommand(todoListId));

		if (response.StatusCode == StatusCodes.Status201Created)
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

		var response = await _mediator.Send(new ShowTodoListQuery(id, filterDueDate));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(TodoListViews.Show, response.Data);

		return BadRequest();
	}
}
