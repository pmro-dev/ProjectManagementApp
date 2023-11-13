using App.Features.Tasks.Common.TaskTags.Common.Interfaces;

namespace App.Features.Tags.Common.Interfaces
{
	public interface ITagDto
	{
		int Id { get; set; }
		ICollection<ITaskTagDto> TaskTags { get; set; }
		string Title { get; set; }
	}
}