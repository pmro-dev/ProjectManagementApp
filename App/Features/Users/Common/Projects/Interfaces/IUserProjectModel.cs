using App.Features.Projects.Common.Models;
using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Users.Common.Projects.Interfaces;

public interface IUserProjectModel
{
	[Timestamp]
	byte[] RowVersion { get; set; }

	[Required]
	public string OwnerId { get; set; }

	[Required]
	public UserModel? Owner { get; set; }

	[Required]
	public Guid ProjectId { get; set; }

	[Required]
	public ProjectModel? Project { get; set; }
}
