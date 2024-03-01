using App.Common.Helpers;
using App.Common.ViewModels;
using App.Common;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using App.Features.Billings.Create;
using App.Features.Billings.Edit;
using App.Features.Billings.Delete;
using App.Features.Billings.Show;
using static App.Common.ControllersConsts;
using static App.Common.Views.ViewsConsts;

namespace App.Features.Billings;

public class BillingController : Controller
{
    private readonly ILogger<BillingController> _logger;
    private readonly IMediator _mediator;

    public BillingController(ILogger<BillingController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Route(CustomRoutes.CreateBilling)]
    public async Task<IActionResult> Create(Guid budgetId)
    {
        ModelStateHelper.SetErrorOnPost(ModelState, TempData);

        var respond = await _mediator.Send(new CreateBillingQuery(budgetId));

        if (respond.StatusCode == StatusCodes.Status200OK)
            return View(respond.Data);

        return BadRequest();
    }

    [HttpPost]
    [Route(CustomRoutes.CreateBilling)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Guid budgetId, CreateBillingInputVM inputVM)
    {
        var response = await _mediator.Send(new CreateBillingCommand(budgetId, inputVM));

        if (response.StatusCode == StatusCodes.Status201Created)
            return RedirectToAction(BillingCtrl.ShowAction, BillingCtrl.Name, response.Data);

        return BadRequest();
    }

    [HttpGet]
    [Route(CustomRoutes.EditBilling)]
    public async Task<IActionResult> Edit(Guid billingId)
    {
        ModelStateHelper.SetErrorOnPost(ModelState, TempData);

        var respond = await _mediator.Send(new EditBillingQuery(billingId));

        if (respond.StatusCode == StatusCodes.Status200OK)
            return View(Basics.Edit, respond.Data);

        return BadRequest();
    }

    [HttpPost]
    [Route(CustomRoutes.EditBilling)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromForm] EditBillingInputVM inputVM)
    {
        var response = await _mediator.Send(new EditBillingCommand(inputVM));

        if (response.StatusCode == StatusCodes.Status201Created)
            return RedirectToAction(BillingCtrl.ShowAction, BillingCtrl.Name, response.Data);

        return BadRequest();
    }

    [HttpGet]
    [Route(CustomRoutes.DeleteBilling)]
    public async Task<IActionResult> Delete(Guid billingId)
    {
        var response = await _mediator.Send(new DeleteBillingQuery(billingId));

        if (response.StatusCode == StatusCodes.Status200OK)
            return View(Basics.Delete, response.Data);

        return BadRequest();
    }

    [HttpPost]
    [Route(CustomRoutes.DeleteBilling)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromForm] WrapperViewModel<DeleteBillingInputVM, DeleteBillingOutputVM> wrapperVM)
    {
		DeleteBillingInputVM deleteInputVM = wrapperVM.InputVM;

        var response = await _mediator.Send(new DeleteBillingCommand(deleteInputVM));

        if (response.StatusCode == StatusCodes.Status200OK)
            return RedirectToAction(BillingCtrl.ShowAction, BillingCtrl.Name, response.Data);

        return BadRequest();
    }

    [HttpGet]
    [Route(CustomRoutes.ShowBilling)]
    public async Task<IActionResult> Show(Guid billingId)
    {
        var response = await _mediator.Send(new ShowBillingQuery(billingId));

        if (response.StatusCode == StatusCodes.Status200OK)
            return View(response.Data);

        return BadRequest();
    }
}
