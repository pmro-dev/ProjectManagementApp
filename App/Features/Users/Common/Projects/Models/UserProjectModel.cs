using App.Features.Projects.Common.Models;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Projects.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Users.Common.Projects.Models;

public class UserProjectModel : IUserProjectModel
{
	[Timestamp]
	public byte[] RowVersion { get; set; }

	[Required]
	public string OwnerId { get; set; }

	[Required]
	public UserModel? Owner { get; set; }

	[Required]
	public Guid ProjectId { get; set; }

	[Required]
	public ProjectModel? Project { get; set; }

	public UserProjectModel(string ownerId, Guid projectId)
	{
		OwnerId = ownerId;
		ProjectId = projectId;
		RowVersion = new byte[] { 1, 1, 1 };
	}
}
