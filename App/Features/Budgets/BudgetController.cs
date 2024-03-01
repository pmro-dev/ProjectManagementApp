using App.Common.Helpers;
using App.Common.ViewModels;
using App.Common;
using App.Features.Tasks.Delete.Models;
using Microsoft.AspNetCore.Mvc;
using static App.Common.ControllersConsts;
using MediatR;
using App.Features.Budgets.Create;
using App.Features.Budgets.Edit;
using App.Features.Budgets.Delete;
using App.Features.Budgets.Show;
using static App.Common.Views.ViewsConsts;

namespace App.Features.Budgets;

public class BudgetController : Controller
{
    private readonly ILogger<BudgetController> _logger;
    private readonly IMediator _mediator;

    public BudgetController(ILogger<BudgetController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Route(CustomRoutes.CreateBudget)]
    public async Task<IActionResult> Create(Guid projectId)
    {
        ModelStateHelper.SetErrorOnPost(ModelState, TempData);

        var respond = await _mediator.Send(new CreateBudgetQuery(projectId));

        if (respond.StatusCode == StatusCodes.Status200OK)
            return View(respond.Data);

        return BadRequest();
    }

    [HttpPost]
    [Route(CustomRoutes.CreateBudget)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Guid projectId, CreateBudgetInputVM inputVM)
    {
        var response = await _mediator.Send(new CreateBudgetCommand(projectId, inputVM));

        if (response.StatusCode == StatusCodes.Status201Created)
            return RedirectToAction(BudgetCtrl.ShowAction, BudgetCtrl.Name, response.Data);

        return BadRequest();
    }

    [HttpGet]
    [Route(CustomRoutes.EditBudget)]
    public async Task<IActionResult> Edit(Guid budgetId)
    {
        ModelStateHelper.SetErrorOnPost(ModelState, TempData);

        var respond = await _mediator.Send(new EditBudgetQuery(budgetId));

        if (respond.StatusCode == StatusCodes.Status200OK)
            return View(Basics.Edit, respond.Data);

        return BadRequest();
    }

    [HttpPost]
    [Route(CustomRoutes.EditBudget)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromForm] EditBudgetInputVM inputVM)
    {
        var response = await _mediator.Send(new EditBudgetCommand(inputVM));

        if (response.StatusCode == StatusCodes.Status201Created)
            return RedirectToAction(BudgetCtrl.ShowAction, BudgetCtrl.Name, response.Data);

        return BadRequest();
    }

    [HttpGet]
    [Route(CustomRoutes.DeleteBudget)]
    public async Task<IActionResult> Delete(Guid budgetId)
    {
        var response = await _mediator.Send(new DeleteBudgetQuery(budgetId));

        if (response.StatusCode == StatusCodes.Status200OK)
            return View(Basics.Delete, response.Data);

        return BadRequest();
    }

    [HttpPost]
    [Route(CustomRoutes.DeleteBudget)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromForm] WrapperViewModel<DeleteBudgetInputVM, DeleteBudgetOutputVM> wrapperVM)
    {
        TaskDeleteInputVM deleteInputVM = wrapperVM.InputVM;

        var response = await _mediator.Send(new DeleteBudgetCommand(deleteInputVM));

        if (response.StatusCode == StatusCodes.Status200OK)
            return RedirectToAction(BudgetCtrl.ShowAction, BudgetCtrl.Name, response.Data);

        return BadRequest();
    }

    [HttpGet]
    [Route(CustomRoutes.ShowBudget)]
    public async Task<IActionResult> Show(Guid budgetId)
    {
        var response = await _mediator.Send(new ShowBudgetQuery(budgetId));

        if (response.StatusCode == StatusCodes.Status200OK)
            return View(response.Data);

        return BadRequest();
    }
}
