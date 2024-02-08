using App.Features.Projects.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Users.Common.Projects.Interfaces;

public interface IUserProjectModel
{
	[Key]
	[Required]
	public string OwnerId { get; set; }

	[Required]
	[ForeignKey(nameof(OwnerId))]
	public UserModel? Owner { get; set; }

	[Required]
	[Key]
	public Guid ProjectId { get; set; }

	[Required]
	[ForeignKey(nameof(ProjectId))]
	public ProjectModel? Project { get; set; }
}
