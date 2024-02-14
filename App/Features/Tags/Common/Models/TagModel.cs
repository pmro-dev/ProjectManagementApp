using App.Features.Budgets.Common.Models;
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.TodoLists.Common.Models;
using App.Features.TodoLists.Common.Tags;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Tags.Common.Models;

public class TagModel : ITagModel
{
    [Key]
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

	[Timestamp]
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	[Required]
    public string Title { get; set; } = string.Empty;

    public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
    public ICollection<TaskTagModel> TaskTags { get; set; } = new List<TaskTagModel>();

	public ICollection<ProjectModel> Projects { get; set; } = new List<ProjectModel>();
	public ICollection<ProjectTagModel> ProjectTags { get; set; } = new List<ProjectTagModel>();

	public ICollection<TodoListModel> TodoLists { get; set; } = new List<TodoListModel>();
	public ICollection<TodoListTagModel> TodoListTags { get; set; } = new List<TodoListTagModel>();

	public ICollection<BudgetModel> Budgets { get; set; } = new List<BudgetModel>();
	public ICollection<BudgetTagModel> BudgetTags { get; set; } = new List<BudgetTagModel>();
}
