using Web.ViewModels.Outputs.Abstract;

namespace Web.ViewModels.Outputs
{
	public class TodoListCreateOutputVM : ITodoListCreateOutputVM
	{
		public string UserId { get; set; } = string.Empty;
	}
}
