namespace Project_DomainEntities
{
	public interface ITagModel
	{
		int Id { get; set; }
		ICollection<ITaskTagModel> TaskTags { get; set; }
		string Title { get; set; }
	}
}