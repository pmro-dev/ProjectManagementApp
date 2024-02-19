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

	public ICollection<UserDto> Clients { get; set; } = new List<UserDto>();

	public ICollection<TodoListDto> TodoLists { get; set; } = new List<TodoListDto>();

	public ICollection<TeamDto> Teams { get; set; } = new List<TeamDto>();
	public ICollection<ProjectTeamDto> ProjectTeams { get; set; } = new List<ProjectTeamDto>();

	public ICollection<TagDto> Tags { get; set; } = new List<TagDto>();
	public ICollection<ProjectTagDto> ProjectTags { get; set; } = new List<ProjectTagDto>();

	public string OwnerId { get; set; } = string.Empty;

	public UserDto? Owner { get; set; }

	public ProjectStatusType Status { get; set; }

	public DateTime Deadline { get; set; }

	public DateTime Created { get; set; }

	public DateTime LastUpdated { get; set; }
}
