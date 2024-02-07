using App.Features.Tasks.Common.TaskTags.Common;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Tags.Common.Interfaces;

public interface ITagModel
{
	[Required]
	[Key]
	int Id { get; set; }

	[Required]
	string DataVersion { get; set; }

	[Required]
	string Title { get; set; }

	ICollection<TaskTagModel> TaskTags { get; set; }
}