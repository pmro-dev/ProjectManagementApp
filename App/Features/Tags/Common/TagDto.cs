using App.Features.Tags.Common.Interfaces;
using App.Features.Tasks.Common.TaskTags.Common.Interfaces;

namespace App.Features.Tags.Common
{
	public class TagDto : ITagDto
	{
		public int Id { get; set; }

		public string Title { get; set; } = string.Empty;

		public ICollection<ITaskTagDto> TaskTags { get; set; } = new List<ITaskTagDto>();
	}
}
