using App.Features.Projects.Common.Interfaces;
using App.Features.Teams.Common.Models;

namespace App.Features.Projects.Common.Models;

public class ProjectTeamDto : IProjectTeamDto
{
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	public Guid ProjectId { get; set; } = Guid.Empty;

	public ProjectModel? Project { get; set; }

	public Guid TeamId { get; set; } = Guid.Empty;

	public TeamModel? Team { get; set; }
}
