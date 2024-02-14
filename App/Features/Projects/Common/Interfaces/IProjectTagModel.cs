using App.Features.Projects.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using App.Features.Tags.Common.Models;

namespace App.Features.Projects.Common.Interfaces;

public interface IProjectTagModel
{
	[Timestamp]
	byte[] RowVersion { get; set; }

	[Required]
	Guid ProjectId { get; set; }

	[ForeignKey(nameof(ProjectId))]
	ProjectModel? Project { get; set; }

	[Required]
	Guid TagId { get; set; }

	[ForeignKey(nameof(TagId))]
	TagModel? Tag { get; set; }
}
