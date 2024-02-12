using App.Features.Projects.Common.Interfaces;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Projects.Common.Models;

public class ProjectTodolistModel : IProjectTodolistModel
{
	[Timestamp]
	public byte[] RowVersion { get; set; }

	[Key]
	[Required]
	public string OwnerId { get; set; }

	[ForeignKey(nameof(OwnerId))]
	public UserModel? Owner { get; set; }

	[Key]
	[Required]
	public Guid ProjectId { get; set; }

	[ForeignKey(nameof(ProjectId))]
	public ProjectModel? Project { get; set; }

	public ProjectTodolistModel(string ownerId)
	{
		OwnerId = ownerId;
		RowVersion = new byte[] { 1, 1, 1 };
	}
}
