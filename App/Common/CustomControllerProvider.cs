namespace App.Common;

using Microsoft.AspNetCore.Mvc.ApplicationModels;

public class CustomControllerProvider : IControllerModelConvention
{
	public void Apply(ControllerModel controller)
	{
		if (IsInFeaturesNamespace(controller.ControllerType.Namespace))
		{
			controller.RouteValues["area"] = "Features";
		}
	}

	private bool IsInFeaturesNamespace(string namespaceName)
	{
		return namespaceName != null && namespaceName.Contains("Features");
	}
}
