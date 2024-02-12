using App.Features.Tasks.Common.TaskTags.Common;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Tags.Common.Interfaces;

public interface ITagModel
{
	[Key]
	[Required]
	Guid Id { get; set; }

	[Timestamp]
	byte[] RowVersion { get; set; }

	[Required]
	string Title { get; set; }

	ICollection<TaskTagModel> TaskTags { get; set; }
}