using System.ComponentModel.DataAnnotations;

namespace Project_DomainEntities
{
	public class TagModel : ITagModel
	{
		[Required]
		[Key]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; } = string.Empty;

		public ICollection<ITaskTagModel> TaskTags { get; set; } = new List<ITaskTagModel>();
	}
}
