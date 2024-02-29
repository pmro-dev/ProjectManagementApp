using App.Common.Helpers;
using App.Common.ViewModels;
using App.Common;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using App.Features.Incomes.Create;
using static App.Common.ControllersConsts;
using App.Features.Incomes.Edit;
using App.Features.Incomes.Delete;
using App.Features.Incomes.Show;
using static App.Common.Views.ViewsConsts;

namespace App.Features.Incomes;

public class IncomeController : Controller
{
    private readonly ILogger<IncomeController> _logger;
    private readonly IMediator _mediator;

    public IncomeController(ILogger<IncomeController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Route(CustomRoutes.CreateIncome)]
    public async Task<IActionResult> Create(Guid budgetId)
    {
        ModelStateHelper.SetErrorOnPost(ModelState, TempData);

        var respond = await _mediator.Send(new CreateIncomeQuery(budgetId));

        if (respond.StatusCode == StatusCodes.Status200OK)
            return View(respond.Data);

        return BadRequest();
    }

    [HttpPost]
    [Route(CustomRoutes.CreateIncome)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Guid budgetId, CreateIncomeInputVM inputVM)
    {
        var response = await _mediator.Send(new CreateIncomeCommand(budgetId, inputVM));

        if (response.StatusCode == StatusCodes.Status201Created)
            return RedirectToAction(IncomeCtrl.ShowAction, IncomeCtrl.Name, response.Data);

        return BadRequest();
    }

    [HttpGet]
    [Route(CustomRoutes.EditIncome)]
    public async Task<IActionResult> Edit(Guid incomeId)
    {
        ModelStateHelper.SetErrorOnPost(ModelState, TempData);

        var respond = await _mediator.Send(new EditIncomeQuery(incomeId));

        if (respond.StatusCode == StatusCodes.Status200OK)
            return View(IncomeViews.Edit, respond.Data);

        return BadRequest();
    }

    [HttpPost]
    [Route(CustomRoutes.EditIncome)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromForm] EditIncomeInputVM inputVM)
    {
        var response = await _mediator.Send(new EditIncomeCommand(inputVM));

        if (response.StatusCode == StatusCodes.Status201Created)
            return RedirectToAction(IncomeCtrl.ShowAction, IncomeCtrl.Name, response.Data);

        return BadRequest();
    }

    [HttpGet]
    [Route(CustomRoutes.DeleteIncome)]
    public async Task<IActionResult> Delete(Guid incomeId)
    {
        var response = await _mediator.Send(new DeleteIncomeQuery(incomeId));

        if (response.StatusCode == StatusCodes.Status200OK)
            return View(IncomeViews.Delete, response.Data);

        return BadRequest();
    }

    [HttpPost]
    [Route(CustomRoutes.DeleteIncome)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromForm] WrapperViewModel<DeleteIncomeInputVM, DeleteIncomeOutputVM> wrapperVM)
    {
		DeleteIncomeInputVM deleteInputVM = wrapperVM.InputVM;

        var response = await _mediator.Send(new DeleteIncomeCommand(deleteInputVM));

        if (response.StatusCode == StatusCodes.Status200OK)
            return RedirectToAction(IncomeCtrl.ShowAction, IncomeCtrl.Name, response.Data);

        return BadRequest();
    }

    [HttpGet]
    [Route(CustomRoutes.ShowIncome)]
    public async Task<IActionResult> Show(Guid incomeId)
    {
        var response = await _mediator.Send(new ShowIncomeQuery(incomeId));

        if (response.StatusCode == StatusCodes.Status200OK)
            return View(response.Data);

        return BadRequest();
    }
}
