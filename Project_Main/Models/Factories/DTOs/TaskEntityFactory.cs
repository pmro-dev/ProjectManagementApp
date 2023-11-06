using Project_DomainEntities;
using Project_Main.Models.DTOs;
using Project_Main.Models.Inputs.DTOs;

namespace Project_Main.Models.Factories.DTOs
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
