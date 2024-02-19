#region USINGS
using App.Features.Budgets.Common.Models;
using App.Features.Projects.Common.Helpers;
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.TodoLists.Common.Models;
using App.Features.Users.Common.Models;
#endregion

namespace App.Features.Projects.Common.Interfaces;

public interface IProjectDto
{
	Guid Id { get; set; }

	byte[] RowVersion { get; set; }

	string Title { get; set; }

	string Description { get; set; }

	Guid BudgetId { get; set; }

	BudgetModel? Budget { get; set; }

	ICollection<UserDto> Clients { get; set; }

	ICollection<TodoListDto> TodoLists { get; set; }

	ICollection<TeamDto> Teams { get; set; }
	ICollection<ProjectTeamDto> ProjectTeams { get; set; }

	ICollection<TagDto> Tags { get; set; }
	ICollection<ProjectTagDto> ProjectTags { get; set; }

	string OwnerId { get; set; }

	UserDto? Owner { get; set; }

	ProjectStatusType Status { get; set; }

	DateTime Deadline { get; set; }

	DateTime Created { get; set; }

	DateTime LastUpdated { get; set; }
}
