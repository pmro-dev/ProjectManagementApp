using App.Features.Projects.Common.Models;
using App.Features.Teams.Common.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Teams.Common.Models;

public class TeamProjectModel : ITeamProjectModel
{
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
