namespace Project_Main.Models.Factories.DTOs
{
	public interface IBaseEntityFactory<ModelType, DTOType> where ModelType : class, new() where DTOType : class, new()
	{
		public ModelType CreateModel();

		public DTOType CreateDto();
	}
}
