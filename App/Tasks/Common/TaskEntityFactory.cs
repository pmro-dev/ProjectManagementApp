using Web.Tags.Common;
using Web.Tasks.Common.Interfaces;
using Web.Tasks.Delete;
using Web.Tasks.Edit;
using Web.TaskTags.Common;

namespace Web.Tasks.Common
{
	public class TaskEntityFactory : ITaskEntityFactory
	{
		public TaskDeleteInputDto CreateDeleteInputDto()
		{
			return new TaskDeleteInputDto();
		}

		public TaskModel CreateModel()
		{
			return new TaskModel();
		}

		public TaskDto CreateDto()
		{
			return new TaskDto();
		}

		public TaskEditInputDto CreateEditInputDto()
		{
			return new TaskEditInputDto();
		}

		public TagDto CreateTagDto()
		{
			return new TagDto();
		}

		public TagModel CreateTagModel()
		{
			return new TagModel();
		}

		public TaskTagDto CreateTaskTagDto()
		{
			return new TaskTagDto();
		}

		public TaskTagModel CreateTaskTagModel()
		{
			return new TaskTagModel();
		}
	}
}
