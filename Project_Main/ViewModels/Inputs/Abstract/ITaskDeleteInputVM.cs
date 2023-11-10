namespace Web.ViewModels.Inputs.Abstract
{
	public interface ITaskDeleteInputVM
	{
		int Id { get; set; }
		int TodoListId { get; set; }
	}
}