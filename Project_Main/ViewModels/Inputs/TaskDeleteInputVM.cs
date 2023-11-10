using Web.ViewModels.Inputs.Abstract;

namespace Web.ViewModels.Inputs
{
	public class TaskDeleteInputVM : ITaskDeleteInputVM
	{
		public int Id { get; set; }
		public int TodoListId { get; set; }
	}
}
