using App.Features.Exceptions.Throw;
using Microsoft.AspNetCore.Mvc.Razor;

namespace App.Common.Views;

public class ViewLocationExpander : IViewLocationExpander
{
    private readonly ILogger<ViewLocationExpander> _logger;

	public ViewLocationExpander(ILogger<ViewLocationExpander> logger)
	{
		_logger = logger;
	}

	public void PopulateValues(ViewLocationExpanderContext context) { }

    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        var featureName = context.ActionContext.ActionDescriptor.DisplayName;

        if (string.IsNullOrEmpty(featureName))
        {
            _logger.LogCritical(ExceptionsMessages.LogPropertyIsNullOrEmpty, nameof(ExpandViewLocations), nameof(featureName));
			throw new InvalidOperationException(ExceptionsMessages.PropertyIsNullOrEmpty(nameof(ExpandViewLocations), nameof(featureName)));
        }

        var featureParts = featureName.Split('.');
        var actionFolder = featureParts[^1].Replace(" (App)", string.Empty);

        var viewPath = $"/{featureParts[1]}/{featureParts[2]}/{actionFolder}/{{0}}.cshtml";

        return new[] { 
                viewPath, 
                "/Common/Views/{0}.cshtml", 
                $"{featureParts[1]}/{featureParts[2]}/{actionFolder}/{{0}}.cshtml", 
				$"{featureParts[1]}/{featureParts[2]}/Common/{{0}}.cshtml",
				$"{featureParts[1]}/Common/{{0}}.cshtml"}
            .Union(viewLocations);
    }
}
