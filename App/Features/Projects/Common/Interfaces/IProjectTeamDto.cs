using App.Features.Projects.Common.Models;
using App.Features.Teams.Common.Models;

namespace App.Features.Projects.Common.Interfaces;

public interface IProjectTeamDto
{
	public byte[] RowVersion { get; set; }

	public Guid ProjectId { get; set; }

	public ProjectModel? Project { get; set; }

	public Guid TeamId { get; set; }

	public TeamModel? Team { get; set; }
}
