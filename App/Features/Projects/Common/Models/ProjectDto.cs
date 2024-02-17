using App.Features.Budgets.Common.Models;
using App.Features.Projects.Common.Helpers;
using App.Features.Projects.Common.Interfaces;
using App.Features.Tags.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;

namespace App.Features.Projects.Common.Models;

public class ProjectDto : IProjectDto
{
	public Guid Id { get; set; }

	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	public string Title { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public Guid BudgetId { get; set; } = Guid.Empty;

	public BudgetModel? Budget { get; set; }

	public ICollection<UserModel> Clients { get; set; } = new List<UserModel>();

	public ICollection<TodoListModel> TodoLists { get; set; } = new List<TodoListModel>();

	public ICollection<TeamModel> Teams { get; set; } = new List<TeamModel>();
	public ICollection<ProjectTeamModel> ProjectTeams { get; set; } = new List<ProjectTeamModel>();

	public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();
	public ICollection<ProjectTagModel> ProjectTags { get; set; } = new List<ProjectTagModel>();

	public string OwnerId { get; set; } = string.Empty;

	public UserModel? Owner { get; set; }

	public ProjectStatusType Status { get; set; }

	public DateTime Deadline { get; set; }

	public DateTime Created { get; set; }

	public DateTime LastUpdated { get; set; }
}
