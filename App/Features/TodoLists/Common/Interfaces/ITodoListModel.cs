using App.Common.Interfaces;
using App.Features.Projects.Common.Models;
using App.Features.Tasks.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.TodoLists.Common.Interfaces;

public interface ITodoListModel : IBasicModelWithTitle
{
	[Required]
	string CreatorId { get; set; }

	[ForeignKey(nameof(CreatorId))]
	UserModel? Creator { get; set; }

	[Required]
	string OwnerId { get; set; }

	[ForeignKey(nameof(OwnerId))]
	UserModel? Owner { get; set; }

	[Required]
	Guid ProjectId { get; set; }

	[ForeignKey(nameof(ProjectId))]
	ProjectModel? Project { get; set; }

	[Required]
	Guid TeamId { get; set; }

	[ForeignKey(nameof(TeamId))]
	TeamModel? Team { get; set; }

	ICollection<TaskModel> Tasks { get; set; }

	bool Equals(object? obj);
	int GetHashCode();
	bool IsTheSame(ITodoListModel obj);
}