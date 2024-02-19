using App.Features.Budgets.Common.Models;
using App.Features.Projects.Common.Models;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.TodoLists.Common.Models;
using App.Features.TodoLists.Common.Tags;

namespace App.Features.Tags.Common.Interfaces
{
	public interface ITagDto
	{
		Guid Id { get; set; }
		string Title { get; set; }
		ICollection<TaskTagDto> TaskTags { get; set; }

		ICollection<ProjectDto> Projects { get; set; }
		ICollection<ProjectTagDto> ProjectTags { get; set; }

		ICollection<TodoListDto> TodoLists { get; set; }
		ICollection<TodoListTagDto> TodoListTags { get; set; }

		ICollection<BudgetDto> Budgets { get; set; }
		ICollection<BudgetTagDto> BudgetTags { get; set; }
	}
}