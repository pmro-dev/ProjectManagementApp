#region USINGS

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static App.Common.Views.ViewsConsts;
using static App.Common.ControllersConsts;
using App.Features.TodoLists.Edit;
using App.Features.TodoLists.Create;
using App.Common.Helpers;
using MediatR;
using App.Features.TodoLists.Show;
using App.Features.TodoLists.Duplicate;
using App.Features.TodoLists.Delete;
using App.Features.TodoLists.Create.Models;
using App.Features.TodoLists.Edit.Models;
using App.Common;
using App.Features.Exceptions.Throw;

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
		ModelStateHelper.SetErrorOnPost(ModelState, TempData);

		var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		var response = await _mediator.Send(new CreateTodoListQuery(userId));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(Basics.Create, response.Data);

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

		if (response.StatusCode == StatusCodesExtension.EntityNameTaken)
		{
			ModelState.AddModelError(string.Empty, response.ErrorMessage!);
			TempData[ModelStateHelper.ModelStateMessageKey] = response.ErrorMessage!;

			return RedirectToAction(TodoListCtrl.CreateAction);
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
	public async Task<IActionResult> Edit(Guid id)
	{
		ModelStateHelper.SetErrorOnPost(ModelState, TempData);

		var response = await _mediator.Send(new EditTodoListQuery(id));

		if (response.StatusCode != StatusCodes.Status200OK)
			return BadRequest();

		return View(Basics.Edit, response.Data);
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
	public async Task<IActionResult> Edit(Guid id, [FromForm] TodoListEditInputVM inputVM)
	{
		var response = await _mediator.Send(new EditTodoListCommand(inputVM, id));

		if (response.StatusCode == StatusCodesExtension.EntityNameTaken)
		{
			ModelState.AddModelError(string.Empty, response.ErrorMessage!);
			TempData[ModelStateHelper.ModelStateMessageKey] = response.ErrorMessage!;

			object routeValues = new { Id = id };
			return RedirectToAction(TodoListCtrl.EditAction, routeValues);
		}

		if (response.StatusCode != StatusCodes.Status201Created)
			return BadRequest();

		return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);
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
	public async Task<IActionResult> Delete(Guid id)
	{
		var response = await _mediator.Send(new DeleteTodoListQuery(id));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(Basics.Delete, response.Data);

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
	public async Task<IActionResult> DeletePost(Guid id)
	{
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
	public async Task<IActionResult> Duplicate(Guid todoListId)
	{
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
	public async Task<IActionResult> Show(Guid id, DateTime? filterDueDate, int? pageNumber, int? itemsPerPageCount)
	{
		int currentPageNumber = pageNumber ?? FirstPageNumber;
		int itemsPerPageAmount = itemsPerPageCount ?? DefaultItemsPerPageCount;

		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Show), currentPageNumber, nameof(currentPageNumber), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Show), itemsPerPageAmount, nameof(itemsPerPageAmount), _logger);

		var response = await _mediator.Send(new ShowTodoListQuery(id, filterDueDate, task => task.Deadline, currentPageNumber, itemsPerPageAmount));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(Basics.Show, response.Data);

		return BadRequest();
	}
}