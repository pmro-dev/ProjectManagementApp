using App.Features.Projects.Common.Models;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Projects.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Users.Common.Projects.Models;

public class UserProjectModel : IUserProjectModel
{
	[Key]
	[Required]
	public string OwnerId { get; set; }

	[Required]
	[ForeignKey(nameof(OwnerId))]
	public UserModel? Owner { get; set; }

	[Key]
	[Required]
	public Guid ProjectId { get; set; }

	[Required]
	[ForeignKey(nameof(ProjectId))]
	public ProjectModel? Project { get; set; }

	public UserProjectModel(string ownerId, Guid projectId)
	{
		OwnerId = ownerId;
		ProjectId = projectId;
	}
}
