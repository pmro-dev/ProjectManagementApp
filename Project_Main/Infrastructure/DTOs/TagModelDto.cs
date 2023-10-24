using Project_DomainEntities;

namespace Project_Main.Infrastructure.DTOs
{
	public class TagModelDto : ITagModel
	{
		public int Id { get; set; }

		public string Title { get; set; } = string.Empty;

		public IEnumerable<ITaskTagModel> TaskTags { get; set; } = new List<TaskTagModelDto>();
	}
}
