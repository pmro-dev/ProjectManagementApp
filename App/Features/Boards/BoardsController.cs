using App.Common;
using App.Features.Boards.All;
using App.Features.Boards.Briefly;
using App.Features.Exceptions.Throw;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Features.Boards;

public class BoardsController : Controller
{
	private readonly IMediator _mediator;
	private readonly ILogger<BoardsController> _logger;
	private const int firstPageNumber = 1;
	private const int defaultItemsPerPageCount = 5;

	public BoardsController(IMediator mediator, ILogger<BoardsController> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	/// <summary>
	/// Action GET with custom route to show All To Do Lists.
	/// </summary>
	/// <returns>All To Do Lists.</returns>
	[HttpGet]
	[Route(CustomRoutes.MainBoardRoute)]
	[Authorize]
	public async Task<IActionResult> Briefly(int? pageNumber, int? itemsPerPageCount)
	{
		int pageNumberTemp = pageNumber ?? firstPageNumber;
		int itemsPerPageCountTemp = itemsPerPageCount ?? defaultItemsPerPageCount;

		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Briefly), pageNumberTemp, nameof(pageNumberTemp), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Briefly), itemsPerPageCountTemp, nameof(itemsPerPageCountTemp), _logger);

		var result = await _mediator.Send(new GetBoardBrieflyQuery(pageNumberTemp, itemsPerPageCountTemp));

		if (result.StatusCode is StatusCodes.Status200OK)
			return View(result.Data);
			
		return BadRequest();
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
	[Route(CustomRoutes.BoardAllRoute)]
	public async Task<IActionResult> All(int? pageNumber, int? itemsPerPageCount)
	{
		int pageNumberTemp = pageNumber ?? firstPageNumber;
		int itemsPerPageCountTemp = itemsPerPageCount ?? defaultItemsPerPageCount;

		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Briefly), pageNumberTemp, nameof(pageNumberTemp), _logger);
		ExceptionsService.WhenValueLowerThanBottomBoundryThrow(nameof(Briefly), itemsPerPageCountTemp, nameof(itemsPerPageCountTemp), _logger);

		var result = await _mediator.Send(new GetBoardAllQuery(pageNumberTemp, itemsPerPageCountTemp));

		if (result.StatusCode is StatusCodes.Status200OK)
			return View(result.Data);
			
		return BadRequest();
	}
}
