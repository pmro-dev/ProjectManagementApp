namespace Project_DomainEntities
{
	public interface ITaskTagModel
	{
		ITagModel Tag { get; set; }
		int TagId { get; set; }
		ITaskModel Task { get; set; }
		int TaskId { get; set; }
	}
}