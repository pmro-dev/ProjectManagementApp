using Web.Tags.Common;
using Web.Tags.Common.Interfaces;
using Web.Tasks.Common;
using Web.Tasks.Common.Interfaces;
using Web.TaskTags.Common.Interfaces;

namespace Web.TaskTags.Common
{
	public class TaskTagDto : ITaskTagDto
	{
		public int TaskId { get; set; }
		public ITaskDto Task { get; set; } = new TaskDto();

		public int TagId { get; set; }
		public ITagDto Tag { get; set; } = new TagDto();
	}
}
