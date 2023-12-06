namespace App.Features.Tasks.Common.TaskTags.Common.Interfaces;

public interface ITaskTagFactory
{
	TaskTagDto CreateTaskTagDto();
	TaskTagModel CreateTaskTagModel();
	TaskTagModel CreateTaskTagModel(TaskTagModel originTaskTag);
}
