using Project_DomainEntities;
using Project_DTO;

namespace Project_Main.Infrastructure.DTOs
{
	public class TaskTagModelDto : ITaskTagModel
	{
		public int TaskId { get; set; }
		public ITaskModel Task { get; set; } = new TaskModelDto();

		public int TagId { get; set; }
		public ITagModel Tag { get; set; } = new TagModelDto();
	}
}
