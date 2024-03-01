using App.Common.Helpers;
using App.Common;
using App.Features.Exceptions.Throw;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static App.Common.ControllersConsts;
using App.Features.Projects.Create.Models;
using App.Features.Projects.Edit.Models;
using App.Features.Projects.Create;
using App.Features.Projects.Edit;
using App.Features.Projects.Delete;
using App.Features.Projects.Show;
using static App.Common.Views.ViewsConsts;

namespace App.Features.Projects;

public class ProjectController : Controller
{
	private readonly ILogger<ProjectController> _logger;
	private readonly IMediator _mediator;

	public ProjectController(ILogger<ProjectController> logger, IMediator mediator)
	{
		_logger = logger;
		_mediator = mediator;
	}

	[HttpGet]
	[Route(CustomRoutes.ProjectCreateRoute)]
	public async Task<IActionResult> Create()
	{
		ModelStateHelper.SetErrorOnPost(ModelState, TempData);

		var respond = await _mediator.Send(new CreateProjectQuery());

		if (respond.StatusCode == StatusCodes.Status200OK)
			return View(respond.Data);

		return BadRequest();
	}

	[HttpPost]
	[Route(CustomRoutes.ProjectCreateRoute)]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(ProjectCreateInputVM inputVM)
	{
		var response = await _mediator.Send(new CreateProjectCommand(inputVM));

		if (response.StatusCode == StatusCodes.Status201Created)
			return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);

		if (response.StatusCode == StatusCodesExtension.EntityNameTaken)
		{
			ModelState.AddModelError(string.Empty, response.ErrorMessage!);
			TempData[ModelStateHelper.ModelStateMessageKey] = response.ErrorMessage!;

			return RedirectToAction(ProjectCtrl.CreateAction);
		}

		return BadRequest();
	}

	[HttpGet]
	[Route(CustomRoutes.ProjectEditRoute)]
	public async Task<IActionResult> Edit(Guid id)
	{
		ModelStateHelper.SetErrorOnPost(ModelState, TempData);

		var response = await _mediator.Send(new EditProjectQuery(id));

		if (response.StatusCode != StatusCodes.Status200OK)
			return BadRequest();

		return View(Basics.Edit, response.Data);
	}

	[HttpPost]
	[Route(CustomRoutes.ProjectEditRoute)]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(Guid id, [FromForm] ProjectEditInputVM inputVM)
	{
		var response = await _mediator.Send(new EditProjectCommand(inputVM, id));

		if (response.StatusCode == StatusCodesExtension.EntityNameTaken)
		{
			ModelState.AddModelError(string.Empty, response.ErrorMessage!);
			TempData[ModelStateHelper.ModelStateMessageKey] = response.ErrorMessage!;

			object routeValues = new { Id = id };
			return RedirectToAction(ProjectCtrl.EditAction, routeValues);
		}

		if (response.StatusCode != StatusCodes.Status201Created)
			return BadRequest();

		//TODO REDIRECT TO MAIN MOARD of projects
		return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);
	}

	[HttpGet]
	[Route(CustomRoutes.ProjectDeleteRoute)]
	public async Task<IActionResult> Delete(Guid id)
	{
		var response = await _mediator.Send(new DeleteProjectQuery(id));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(Basics.Delete, response.Data);

		return BadRequest();
	}

	[HttpPost]
	[Route(CustomRoutes.ProjectDeletePostRoute)]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeletePost(Guid id)
	{
		var response = await _mediator.Send(new DeleteProjectCommand(id));

		//TODO REDIRECT TO MAIN BOARD OF PROJECTS
		if (response.StatusCode == StatusCodes.Status200OK)
			return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);

		return BadRequest();
	}

	[HttpGet]
	[Route(CustomRoutes.ProjectShowTodoListsBoardRoute)]
	public async Task<IActionResult> ShowTodoLists(Guid id, int? pageNumber, int? itemsPerPageCount)
	{
		int currentPageNumber = pageNumber ?? FirstPageNumber;
		int itemsPerPageAmount = itemsPerPageCount ?? DefaultItemsPerPageCount;

		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(ShowTodoLists), currentPageNumber, nameof(currentPageNumber), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(ShowTodoLists), itemsPerPageAmount, nameof(itemsPerPageAmount), _logger);

		//TODO
		// Here is specified selector for sorting to do lists by progress made but in the final version, user should choose: sort by the name, best or worst progress... 
		var response = await _mediator.Send(new ShowProjectTodoListsQuery(id, brieflyTodoList => brieflyTodoList.ProgressMade, currentPageNumber, itemsPerPageAmount));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(Basics.Show, response.Data);

		return BadRequest();
	}

	[HttpGet]
	[Route(CustomRoutes.ProjectShowStatisticsBoardRoute)]
	public async Task<IActionResult> ShowStatistics(Guid id, int? pageNumber, int? itemsPerPageCount)
	{
		int currentPageNumber = pageNumber ?? FirstPageNumber;
		int itemsPerPageAmount = itemsPerPageCount ?? DefaultItemsPerPageCount;

		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(ShowStatistics), currentPageNumber, nameof(currentPageNumber), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(ShowStatistics), itemsPerPageAmount, nameof(itemsPerPageAmount), _logger);

		//TODO
		// Here is specified selector for sorting to do lists, in the final version, user should choose: sort by the name, best or worst progress... 
		// Here is specified selector for sorting teams, in the final version, user should choose: sort by the name, best or worst progress... 
		var response = await _mediator.Send(new ShowProjectStatisticsQuery(id, brieflyTodoList => brieflyTodoList.ProgressMade, brieflyTeams => brieflyTeams.Name, currentPageNumber, itemsPerPageAmount));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(Basics.Show, response.Data);

		return BadRequest();
	}

	[HttpGet]
	[Route(CustomRoutes.ProjectShowTeamsBoardRoute)]
	public async Task<IActionResult> ShowTeams(Guid id, int? pageNumber, int? itemsPerPageCount)
	{
		int currentPageNumber = pageNumber ?? FirstPageNumber;
		int itemsPerPageAmount = itemsPerPageCount ?? DefaultItemsPerPageCount;

		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(ShowTeams), currentPageNumber, nameof(currentPageNumber), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(ShowTeams), itemsPerPageAmount, nameof(itemsPerPageAmount), _logger);

		//TODO
		// Here is specified selector for sorting to do lists, in the final version, user should choose: sort by the name, best or worst progress... 
		// Here is specified selector for sorting teams, in the final version, user should choose: sort by the name, best or worst progress... 
		var response = await _mediator.Send(new ShowProjectTeamsQuery(id, team => team.Name, currentPageNumber, itemsPerPageAmount));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(Basics.Show, response.Data);

		return BadRequest();
	}
}