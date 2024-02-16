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
		public ICollection<ProjectModel> Projects { get; set; } = new List<ProjectModel>();
		public ICollection<ProjectTagModel> ProjectTags { get; set; } = new List<ProjectTagModel>();
		public ICollection<TodoListModel> TodoLists { get; set; } = new List<TodoListModel>();
		public ICollection<TodoListTagModel> TodoListTags { get; set; } = new List<TodoListTagModel>();
		public ICollection<BudgetModel> Budgets { get; set; } = new List<BudgetModel>();
		public ICollection<BudgetTagModel> BudgetTags { get; set; } = new List<BudgetTagModel>();
	}
}
