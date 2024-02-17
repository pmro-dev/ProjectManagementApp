using App.Features.Projects.Common.Interfaces;
using App.Features.Tags.Common.Models;

namespace App.Features.Projects.Common.Models;

public class ProjectTagDto : IProjectTagDto
{
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	public Guid ProjectId { get; set; } = Guid.Empty;

	public ProjectModel? Project { get; set; }

	public Guid TagId { get; set; } = Guid.Empty;

	public TagModel? Tag { get; set; }
}
