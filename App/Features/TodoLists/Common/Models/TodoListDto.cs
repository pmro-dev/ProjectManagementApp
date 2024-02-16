#region USINGS
using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Models;
using App.Features.Teams.Common.Models;
using App.Features.TodoLists.Common.Interfaces;
using App.Features.TodoLists.Common.Tags;
using App.Features.Users.Common.Models;
#endregion

namespace App.Features.TodoLists.Common.Models;

public class TodoListDto : ITodoListDto
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };
	public string Title { get; set; } = string.Empty;

	public string OwnerId { get; set; } = string.Empty;
	public UserModel? Owner { get; set; }

	public string CreatorId { get; set; } = string.Empty;
	public UserModel? Creator { get; set; }

	public Guid ProjectId { get; set; } = Guid.Empty;
	public ProjectModel? Project { get; set; }

	public Guid TeamId { get; set; } = Guid.Empty;
	public TeamModel? Team { get; set; }

	public ICollection<TaskDto> Tasks { get; set; } = new List<TaskDto>();

	public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();
	public ICollection<TodoListTagModel> TodoListTags { get; set; } = new List<TodoListTagModel>();
}
