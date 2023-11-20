using App.Features.Boards.All;
using App.Features.Boards.Briefly;
using App.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Features.Boards;

public class BoardsController : Controller
{
	private readonly IMediator _mediator;

	public BoardsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	/// <summary>
	/// Action GET with custom route to show All To Do Lists.
	/// </summary>
	/// <returns>All To Do Lists.</returns>
	[HttpGet]
	[Route(CustomRoutes.MainBoardRoute)]
	[Authorize]
	public async Task<IActionResult> Briefly()
	{
		var result = await _mediator.Send(new GetBoardBrieflyQuery());

		return View(result);
	}

	/// <summary>
	/// Action GET to (get) ALL To Do Lists.
	/// </summary>
	/// <returns>
	/// Return different view based on the final result. 
	/// Return: Not Found when there isn't any object for To Do Lists,
	/// or view with data.
	/// </returns>
	[HttpGet]
	[Route(CustomRoutes.AllDetailsRoute)]
	public async Task<IActionResult> All()
	{
		var result = await _mediator.Send(new GetBoardAllQuery());

		return View(result);
	}
}
