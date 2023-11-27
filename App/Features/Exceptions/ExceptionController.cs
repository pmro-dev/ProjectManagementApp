using Microsoft.AspNetCore.Mvc;
using static App.Common.Views.ViewsConsts;

namespace App.Features.Exceptions;

public class ExceptionController : Controller
{
	public IActionResult Show(string ExceptionMessage)
	{
		return View(ExceptionViews.Error, ExceptionMessage);
	}
}
