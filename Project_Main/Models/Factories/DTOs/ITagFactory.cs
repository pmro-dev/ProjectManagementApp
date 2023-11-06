using Project_DomainEntities;
using Project_Main.Models.DTOs;

namespace Project_Main.Models.Factories.DTOs
{
	public interface ITagFactory
	{
		TagDto CreateTagDto();
		TagModel CreateTagModel();
	}
}
