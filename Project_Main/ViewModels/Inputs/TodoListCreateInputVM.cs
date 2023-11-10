using Project_Main.ViewModels.Inputs.Abstract;

namespace Web.ViewModels.Inputs
{
	public class TodoListCreateInputVM : ITodoListCreateInputVM
	{
		public string UserId { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
	}
}