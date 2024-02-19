using App.Features.Budgets.Common.Models;
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.TodoLists.Common.Models;
using App.Features.TodoLists.Common.Tags;

namespace App.Features.Tags.Common.Models
{
    public class TagDto : ITagDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = string.Empty;

        public ICollection<TaskTagDto> TaskTags { get; set; } = new List<TaskTagDto>();
		public ICollection<ProjectDto> Projects { get; set; } = new List<ProjectDto>();
		public ICollection<ProjectTagDto> ProjectTags { get; set; } = new List<ProjectTagDto>();
		public ICollection<TodoListDto> TodoLists { get; set; } = new List<TodoListDto>();
		public ICollection<TodoListTagDto> TodoListTags { get; set; } = new List<TodoListTagDto>();
		public ICollection<BudgetDto> Budgets { get; set; } = new List<BudgetDto>();
		public ICollection<BudgetTagDto> BudgetTags { get; set; } = new List<BudgetTagDto>();
	}
}
