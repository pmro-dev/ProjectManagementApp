using App.Features.Projects.Common.Models;
using App.Features.Tags.Common.Models;

namespace App.Features.Projects.Common.Interfaces;

public interface IProjectTagDto
{
	byte[] RowVersion { get; set; }

	Guid ProjectId { get; set; }

	ProjectModel? Project { get; set; }

	Guid TagId { get; set; }

	TagModel? Tag { get; set; }
}
