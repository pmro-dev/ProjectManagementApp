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

	ICollection<UserModel> Clients { get; set; }

	ICollection<TodoListModel> TodoLists { get; set; }

	ICollection<TeamModel> Teams { get; set; }
	ICollection<ProjectTeamModel> ProjectTeams { get; set; }

	ICollection<TagModel> Tags { get; set; }
	ICollection<ProjectTagModel> ProjectTags { get; set; }

	string OwnerId { get; set; }

	UserModel? Owner { get; set; }

	ProjectStatusType Status { get; set; }

	DateTime Deadline { get; set; }

	DateTime Created { get; set; }

	DateTime LastUpdated { get; set; }
}
