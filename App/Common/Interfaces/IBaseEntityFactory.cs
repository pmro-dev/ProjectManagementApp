namespace Web.Common.Interfaces;

public interface IBaseEntityFactory<out ModelType, out DTOType> where ModelType : class, new() where DTOType : class, new()
{
	public ModelType CreateModel();

	public DTOType CreateDto();
}
