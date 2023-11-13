using App.Features.Tags.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Tags.Common;

public class TagModel : ITagModel
{
	[Required]
	[Key]
	public int Id { get; set; }

	[Required]
	public string Title { get; set; } = string.Empty;

	public ICollection<ITaskTagModel> TaskTags { get; set; } = new List<ITaskTagModel>();
}
