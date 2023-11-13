using App.Features.Tasks.Common.TaskTags.Common.Interfaces;

namespace App.Features.Tags.Common.Interfaces;

public interface ITagModel
{
	int Id { get; set; }
	ICollection<ITaskTagModel> TaskTags { get; set; }
	string Title { get; set; }
}