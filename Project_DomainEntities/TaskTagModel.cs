namespace Project_DomainEntities
{
	public class TaskTagModel : ITaskTagModel
	{
		public int TaskId { get; set; }
		public ITaskModel Task { get; set; } = new TaskModel();

		public int TagId { get; set; }
		public ITagModel Tag { get; set; } = new TagModel();
	}
}
