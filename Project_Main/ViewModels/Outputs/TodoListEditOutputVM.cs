using Web.ViewModels.Outputs.Abstract;

namespace Web.ViewModels.Outputs
{
	public class TodoListEditOutputVM : ITodoListEditOutputVM
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string UserId { get; set; } = string.Empty;
	}
}