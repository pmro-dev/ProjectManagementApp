using App.Features.Tasks.Common.TaskTags.Common;

namespace App.Features.Tags.Common.Interfaces;

public interface ITagModel
{
	int Id { get; set; }
	ICollection<TaskTagModel> TaskTags { get; set; }
	string Title { get; set; }
}