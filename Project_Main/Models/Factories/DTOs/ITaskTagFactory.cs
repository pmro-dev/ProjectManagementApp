
using Project_DomainEntities;
using Project_Main.Models.DTOs;

namespace Project_Main.Models.Factories.DTOs
{
	public interface ITaskTagFactory
	{
		TaskTagDto CreateTaskTagDto();
		TaskTagModel CreateTaskTagModel();
	}
}
