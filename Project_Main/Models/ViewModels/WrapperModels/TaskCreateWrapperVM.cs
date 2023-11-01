using Project_Main.Models.ViewModels.InputModels;
using Project_Main.Models.ViewModels.OutputModels;

namespace Project_Main.Models.ViewModels.WrapperModels
{
    public class TaskCreateWrapperVM : ITaskCreateWrapperVM
    {
        public ITaskCreationInputVM InputVM { get; set; } = new TaskCreationInputVM();

        public ITaskCreationOutputVM OutputVM { get; set; } = new TaskCreationOutputVM();
    }
}
