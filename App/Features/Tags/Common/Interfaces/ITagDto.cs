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

		ICollection<ProjectModel> Projects { get; set; }
		ICollection<ProjectTagModel> ProjectTags { get; set; }

		ICollection<TodoListModel> TodoLists { get; set; }
		ICollection<TodoListTagModel> TodoListTags { get; set; }

		ICollection<BudgetModel> Budgets { get; set; }
		ICollection<BudgetTagModel> BudgetTags { get; set; }
	}
}