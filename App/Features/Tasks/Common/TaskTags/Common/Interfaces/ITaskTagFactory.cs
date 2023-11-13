using App.Features.Tasks.Common.TaskTags.Common;

namespace App.Features.Tasks.Common.TaskTags.Common.Interfaces;

public interface ITaskTagFactory
{
	TaskTagDto CreateTaskTagDto();
	TaskTagModel CreateTaskTagModel();
}
