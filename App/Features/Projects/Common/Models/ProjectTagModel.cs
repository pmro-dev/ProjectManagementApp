using App.Features.Projects.Common.Interfaces;
using App.Features.Tags.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Projects.Common.Models;

public class ProjectTagModel : IProjectTagModel
{
	[Timestamp]
	public byte[] RowVersion { get; set; }

	[Required]
	public Guid ProjectId { get; set; }

	[ForeignKey(nameof(ProjectId))]
	public ProjectModel? Project { get; set; }

	[Required]
	public Guid TagId { get; set; }

	[ForeignKey(nameof(TagId))]
	public TagModel? Tag { get; set; }

	public ProjectTagModel(Guid projectId, Guid tagId)
	{
		ProjectId = projectId;
		TagId = tagId;
		RowVersion = new byte[] { 1, 1, 1 };
	}
}
