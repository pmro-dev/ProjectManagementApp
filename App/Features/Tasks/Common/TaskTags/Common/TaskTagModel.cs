using App.Features.Tasks.Common.TaskTags.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.Tags.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Tasks.Common.TaskTags.Common;

public class TaskTagModel : ITaskTagModel
{
	[Timestamp]
	public byte[] RowVersion { get; set; } = { 1, 1, 1 };

	[Required]
	public Guid TaskId { get; set; } = Guid.NewGuid();

	[ForeignKey(nameof(TaskId))]
	public TaskModel Task { get; set; } = new TaskModel();

	[Required]
	public Guid TagId { get; set; } = Guid.NewGuid();

	[ForeignKey(nameof(TagId))]
	public TagModel Tag { get; set; } = new TagModel();
}
