#region USINGS
using App.Common.Interfaces;
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.TodoLists.Common.Tags;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace App.Features.TodoLists.Common.Interfaces;

public interface ITodoListModel : IBasicModelWithTitle
{
	string? CreatorId { get; set; }
	//UserModel? Creator { get; set; }

	string? OwnerId { get; set; }
	//UserModel? Owner { get; set; }

	[Required]
	Guid ProjectId { get; set; }

	[ForeignKey(nameof(ProjectId))]
	ProjectModel? Project { get; set; }

	[Required]
	Guid TeamId { get; set; }

	[ForeignKey(nameof(TeamId))]
	TeamModel? Team { get; set; }

	ICollection<TaskModel> Tasks { get; set; }

	ICollection<TagModel> Tags { get; set; }
	ICollection<TodoListTagModel> TodoListTags { get; set; }

	bool Equals(object? obj);
	int GetHashCode();
	bool IsTheSame(ITodoListModel obj);
}