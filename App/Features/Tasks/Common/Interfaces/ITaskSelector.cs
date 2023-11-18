using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Features.Tasks.Common.Interfaces;

public interface ITaskSelector
{
	SelectList Create(TaskDto taskDto);
}
