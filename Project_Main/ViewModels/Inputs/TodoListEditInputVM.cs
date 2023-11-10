using Web.ViewModels.Inputs.Abstract;

namespace Web.ViewModels.Inputs
{
	public class TodoListEditInputVM : ITodoListEditInputVM
	{
		public string Title { get; set; } = string.Empty;
	}
}