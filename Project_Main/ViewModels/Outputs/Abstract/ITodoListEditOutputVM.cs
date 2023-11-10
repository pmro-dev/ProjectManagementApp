namespace Web.ViewModels.Outputs.Abstract
{
	public interface ITodoListEditOutputVM
	{
		int Id { get; set; }
		string Title { get; set; }
		string UserId { get; set; }
	}
}