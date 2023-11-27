using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static App.Common.ControllersConsts;

namespace App.Features.Exceptions.Handle;

public class ExceptionCustomFilter : IExceptionFilter
{
	public void OnException(ExceptionContext context)
	{
		string exceptionMessage = context.Exception.Message;
		context.Result = new RedirectToActionResult(ExceptionCtrl.ShowAction, ExceptionCtrl.Name, new { ExceptionMessage = exceptionMessage });
		context.ExceptionHandled = true;
	}
}
