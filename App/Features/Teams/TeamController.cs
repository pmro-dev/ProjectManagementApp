using App.Common.Helpers;
using App.Common;
using App.Features.Exceptions.Throw;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static App.Common.ControllersConsts;
using static App.Common.Views.ViewsConsts;
using App.Features.Projects.Show;
using App.Features.Teams.Common.Create;
using App.Features.TodoLists.Create;
using App.Features.Teams.Common.Edit;
using App.Features.Teams.Common.Delete;
using App.Features.Teams.Common.Show;

namespace App.Features.Teams;

public class TeamController : Controller
{
	private readonly IMediator _mediator;
	private readonly ILogger<TeamController> _logger;

	public TeamController(IMediator mediator, ILogger<TeamController> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	// Create Team for specific project GET
	[HttpGet]
	[Route(CustomRoutes.CreateTeamWithinProjectScope)]
	public async Task<IActionResult> Create(Guid projectId)
	{
		ModelStateHelper.SetModelStateErrorMessageWhenSomeHappendOnPost(ModelState, TempData);

		var response = await _mediator.Send(new CreateTeamWithinProjectQuery(projectId));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(TeamViews.Create, response.Data);

		return BadRequest();
	}

	// Create Team for specific project POST
	[HttpPost]
	[ValidateAntiForgeryToken]
	[Route(CustomRoutes.CreateTeamWithinProjectScope)]
	public async Task<IActionResult> Create(CreateTeamWithinProjectInputVM inputVM)
	{
		var response = await _mediator.Send(new CreateTeamWithinProjectCommand(inputVM));

		if (response.StatusCode == StatusCodes.Status201Created)
			return RedirectToAction(ProjectCtrl.ShowAction, ProjectCtrl.Name, new { id = inputVM.ProjectId });

		if (response.StatusCode == StatusCodesExtension.EntityNameTaken)
		{
			ModelState.AddModelError(string.Empty, response.ErrorMessage!);
			TempData[ModelStateHelper.ModelStateMessageKey] = response.ErrorMessage!;

			return RedirectToAction(TeamCtrl.CreateAction);
		}

		return BadRequest();
	}

	// Create Team as reusable Scheme GET
	[HttpGet]
	[Route(CustomRoutes.CreateTeamScheme)]
	public async Task<IActionResult> Create()
	{
		ModelStateHelper.SetModelStateErrorMessageWhenSomeHappendOnPost(ModelState, TempData);

		var response = await _mediator.Send(new CreateTeamAsSchemeQuery());

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(TeamViews.Create, response.Data);

		return BadRequest();
	}

	// Create Team as reusable Scheme POST
	[HttpPost]
	[ValidateAntiForgeryToken]
	[Route(CustomRoutes.CreateTeamScheme)]
	public async Task<IActionResult> Create(CreateTeamAsSchemeInputVM inputVM)
	{
		var response = await _mediator.Send(new CreateTeamAsSchemeCommand(inputVM));

		if (response.StatusCode == StatusCodes.Status201Created)
			return RedirectToAction(ProjectCtrl.ShowAction, ProjectCtrl.Name, new { id = inputVM.ProjectId });

		if (response.StatusCode == StatusCodesExtension.EntityNameTaken)
		{
			ModelState.AddModelError(string.Empty, response.ErrorMessage!);
			TempData[ModelStateHelper.ModelStateMessageKey] = response.ErrorMessage!;

			return RedirectToAction(TeamCtrl.CreateAction);
		}

		return BadRequest();
	}

	[HttpGet]
	[Route(CustomRoutes.EditTeamWithinProjectScope)]
	public async Task<IActionResult> Edit(Guid projectId, Guid teamId)
	{
		// TODO write GUID exception valudation
		//ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Edit), id, nameof(id), _logger);
		ModelStateHelper.SetModelStateErrorMessageWhenSomeHappendOnPost(ModelState, TempData);

		var response = await _mediator.Send(new EditTeamWithinProjectScopeQuery(projectId, teamId));

		if (response.StatusCode != StatusCodes.Status200OK)
			return BadRequest();

		return View(TeamViews.Edit, response.Data);
	}

	[HttpPost]
	[Route(CustomRoutes.EditTeamWithinProjectScope)]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(Guid projectId, Guid teamId, [FromForm] EditTeamInputVM inputVM)
	{
		// TODO write GUID exception valudation
		//ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Edit), id, nameof(id), _logger);
		//ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Edit), inputVM.Id, nameof(inputVM.Id), _logger);

		var response = await _mediator.Send(new EditTeamWithinProjectScopeCommand(projectId, teamId, inputVM));

		if (response.StatusCode == StatusCodesExtension.EntityNameTaken)
		{
			ModelState.AddModelError(string.Empty, response.ErrorMessage!);
			TempData[ModelStateHelper.ModelStateMessageKey] = response.ErrorMessage!;

			return RedirectToAction(TeamCtrl.EditAction, new { Id = teamId });
		}

		if (response.StatusCode != StatusCodes.Status201Created)
			return BadRequest();

		// TODO should return to the parent url / view which invoke this action
		return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);
	}

	[HttpGet]
	[Route(CustomRoutes.EditTeamScheme)]
	public async Task<IActionResult> Edit(Guid teamId)
	{
		// TODO write GUID exception valudation
		//ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Edit), id, nameof(id), _logger);
		ModelStateHelper.SetModelStateErrorMessageWhenSomeHappendOnPost(ModelState, TempData);

		var response = await _mediator.Send(new EditTeamSchemeQuery(teamId));

		if (response.StatusCode != StatusCodes.Status200OK)
			return BadRequest();

		return View(TeamViews.Edit, response.Data);
	}

	[HttpPost]
	[Route(CustomRoutes.EditTeamScheme)]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(Guid teamId, [FromForm] EditTeamInputVM inputVM)
	{
		// TODO write GUID exception valudation
		//ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Edit), id, nameof(id), _logger);
		//ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Edit), inputVM.Id, nameof(inputVM.Id), _logger);

		var response = await _mediator.Send(new EditTeamSchemeCommand(teamId, inputVM));

		if (response.StatusCode == StatusCodesExtension.EntityNameTaken)
		{
			ModelState.AddModelError(string.Empty, response.ErrorMessage!);
			TempData[ModelStateHelper.ModelStateMessageKey] = response.ErrorMessage!;

			return RedirectToAction(TeamCtrl.EditAction, new { Id = teamId });
		}

		if (response.StatusCode != StatusCodes.Status201Created)
			return BadRequest();

		// TODO should return to the parent url / view which invoke this action
		return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);
	}

	[HttpGet]
	[Route(CustomRoutes.DeleteTeamWithinProjectScope)]
	public async Task<IActionResult> Delete(Guid projectId, Guid teamId)
	{
		// TODO write GUID exception valudation
		//ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Delete), id, nameof(id), _logger);

		var response = await _mediator.Send(new DeleteTeamWithinProjectScopeQuery(projectId, teamId));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(TeamViews.Delete, response.Data);

		return BadRequest();
	}

	[HttpPost]
	[Route(CustomRoutes.DeleteTeamWithinProjectScope)]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeletePost(Guid projectId, Guid teamId)
	{
		// TODO write GUID exception valudation
		//ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(DeletePost), id, nameof(id), _logger);

		var response = await _mediator.Send(new DeleteTeamWithinProjectScopeCommand(projectId, teamId));

		// TODO should return to the parent url / view which invoke this action
		if (response.StatusCode == StatusCodes.Status200OK)
			return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);

		return BadRequest();
	}

	[HttpGet]
	[Route(CustomRoutes.DeleteTeamScheme)]
	public async Task<IActionResult> Delete(Guid teamId)
	{
		// TODO write GUID exception valudation
		//ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Delete), id, nameof(id), _logger);

		var response = await _mediator.Send(new DeleteTeamSchemeQuery(teamId));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(TeamViews.Delete, response.Data);

		return BadRequest();
	}

	[HttpPost]
	[Route(CustomRoutes.DeleteTeamScheme)]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeletePost(Guid teamId)
	{
		// TODO write GUID exception valudation
		//ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(DeletePost), id, nameof(id), _logger);

		var response = await _mediator.Send(new DeleteTeamSchemeCommand(teamId));

		// TODO should return to the parent url / view which invoke this action
		if (response.StatusCode == StatusCodes.Status200OK)
			return RedirectToAction(BoardsCtrl.BrieflyAction, BoardsCtrl.Name);

		return BadRequest();
	}

	[HttpGet]
	[Route(CustomRoutes.ShowTeamsSchemes)]
	public async Task<IActionResult> Show(int? pageNumber, int? itemsPerPageCount)
	{
		int currentPageNumber = pageNumber ?? FirstPageNumber;
		int itemsPerPageAmount = itemsPerPageCount ?? DefaultItemsPerPageCount;

		// TODO write GUID exception valudation
		//ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Show), id, nameof(id), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Show), currentPageNumber, nameof(currentPageNumber), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Show), itemsPerPageAmount, nameof(itemsPerPageAmount), _logger);

		var response = await _mediator.Send(new ShowTeamsSchemesQuery(currentPageNumber, itemsPerPageAmount, team => team.Title));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(TeamViews.Show, response.Data);

		return BadRequest();
	}

	[HttpGet]
	[Route(CustomRoutes.ShowTeamsOfProject)]
	public async Task<IActionResult> ShowTeams(Guid projectId, int? pageNumber, int? itemsPerPageCount)
	{
		int currentPageNumber = pageNumber ?? FirstPageNumber;
		int itemsPerPageAmount = itemsPerPageCount ?? DefaultItemsPerPageCount;

		// TODO write GUID exception valudation
		//ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Show), id, nameof(id), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(ShowTeams), currentPageNumber, nameof(currentPageNumber), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(ShowTeams), itemsPerPageAmount, nameof(itemsPerPageAmount), _logger);

		//TODO
		// Here is specified selector for sorting to do lists, in the final version, user should choose: sort by the name, best or worst progress... 
		// Here is specified selector for sorting teams, in the final version, user should choose: sort by the name, best or worst progress... 
		var response = await _mediator.Send(new ShowTeamsOfProjectQuery(projectId, team => team.Name, currentPageNumber, itemsPerPageAmount));

		if (response.StatusCode == StatusCodes.Status200OK)
			return View(ProjectViews.Show, response.Data);

		return BadRequest();
	}
}
