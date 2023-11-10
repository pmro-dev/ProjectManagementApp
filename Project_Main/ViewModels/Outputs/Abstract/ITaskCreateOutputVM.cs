namespace Web.ViewModels.Outputs.Abstract
{
	public interface ITaskCreateOutputVM
	{
		int TodoListId { get; set; }
		string TodoListName { get; set; }
		string UserId { get; set; }
	}
}