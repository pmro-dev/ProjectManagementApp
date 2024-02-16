using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.TodoLists.Common.Tags;
using App.Features.Users.Common.Models;

namespace App.Features.TodoLists.Common.Interfaces;

public interface ITodoListDto
{
	Guid Id { get; set; }
	byte[] RowVersion { get; set; }
	string Title { get; set; }

	string OwnerId { get; set; }
	UserModel? Owner { get; set; }

	string CreatorId { get; set; }
	UserModel? Creator { get; set; }

	Guid ProjectId { get; set; }
	ProjectModel? Project { get; set; }

	Guid TeamId { get; set; }
	TeamModel? Team { get; set; }

	ICollection<TaskDto> Tasks { get; set; }

	ICollection<TagModel> Tags { get; set; }
	ICollection<TodoListTagModel> TodoListTags { get; set; }
}