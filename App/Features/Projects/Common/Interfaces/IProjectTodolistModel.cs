using App.Features.Projects.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Projects.Common.Interfaces;

public interface IProjectTodolistModel
{
	[Timestamp]
	byte[] RowVersion { get; set; }

	[Required]
	string OwnerId { get; set; }

	[ForeignKey(nameof(OwnerId))]
	UserModel? Owner { get; set; }

	[Required]
	Guid ProjectId { get; set; }

	[ForeignKey(nameof(ProjectId))]
	ProjectModel? Project { get; set; }
}
