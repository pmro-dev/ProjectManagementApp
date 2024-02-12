using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Tasks.Common.TaskTags.Common.Interfaces;

public interface ITaskTagModel
{
	[Timestamp]
	byte[] RowVersion { get; set; }

	[Required]
	Guid TagId { get; set; }

	[ForeignKey(nameof(TagId))]
	TagModel Tag { get; set; }

	[Required]
	Guid TaskId { get; set; }

	[ForeignKey(nameof(TaskId))]
	TaskModel Task { get; set; }
}