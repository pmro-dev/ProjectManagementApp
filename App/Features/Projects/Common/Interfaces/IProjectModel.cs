#region USINGS
using App.Features.Budgets.Common.Models;
using App.Features.Projects.Common.Helpers;
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.TodoLists.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace App.Features.Projects.Common.Interfaces;

interface IProjectModel
{
	[Key]
	[Required]
	Guid Id { get; set; }

	[Timestamp]
	byte[] RowVersion { get; set; }

	[Required]
	string Title { get; set; }

	[Required]
	string Description { get; set; }

	[Required]
	Guid BudgetId { get; set; }

	[Required]
	[ForeignKey(nameof(BudgetId))]
	BudgetModel? Budget { get; set; }

	ICollection<ProjectUserModel> ProjectClients { get; set; }

	ICollection<TodoListModel> TodoLists { get; set; }

	ICollection<TeamModel> Teams { get; set; }
	ICollection<ProjectTeamModel> ProjectTeams { get; set; }

	ICollection<TagModel> Tags { get; set; }
	ICollection<ProjectTagModel> ProjectTags { get; set; }

	string? OwnerId { get; set; }

	[Required]
	ProjectStatusType Status { get; set; }

	[Required]
	DateTime Deadline { get; set; }

	[Required]
	DateTime Created { get; set; }

	[Required]
	DateTime LastUpdated { get; set; }
}