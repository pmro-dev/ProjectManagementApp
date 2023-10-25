using Project_Main.Models.ViewModels.InputModels;
using Project_Main.Models.ViewModels.OutputModels;

namespace Project_Main.Models.ViewModels.WrapperModels
{
	public class TaskCreateWrapperVM
	{
		public TaskCreateInputVM InputVM { get; set; } = new();

		public TaskCreateOutputVM OutputVM { get; set; } = new();
	}
}
