using App.Features.Projects.Common.Interfaces;
using App.Features.Teams.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Projects.Common.Models;

public class ProjectTeamModel : IProjectTeamModel
{
	[Timestamp]
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	[Key]
	[Required]
	public Guid ProjectId { get; set; }

	[ForeignKey(nameof(ProjectId))]
	public ProjectModel? Project { get; set; }

	[Key]
	[Required]
	public Guid TeamId { get; set; }

	[ForeignKey(nameof(TeamId))]
	public TeamModel? Team { get; set; }
}
