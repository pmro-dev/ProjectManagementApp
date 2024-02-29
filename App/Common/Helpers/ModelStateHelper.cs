using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace App.Common.Helpers;

public static class ModelStateHelper
{
	public const string ModelStateMessageKey = "ModelStateErrorMessage";

	public static void SetErrorOnPost(ModelStateDictionary modelState, ITempDataDictionary tempData)
	{
		if (tempData.TryGetValue(ModelStateMessageKey, out var modelErrorMessage) && modelErrorMessage != null)
		{
			string errorMessage = (string)modelErrorMessage;
			modelState.AddModelError(string.Empty, errorMessage);
		}
	}
}
