using Project_Main.Models.ViewModels.InputModels;
using Project_Main.Models.ViewModels.OutputModels;

namespace Project_Main.Models.ViewModels.WrapperModels
{
    public interface ITaskCreateWrapperVM
    {
        ITaskCreationInputVM InputVM { get; set; }
        ITaskCreationOutputVM OutputVM { get; set; }
    }
}