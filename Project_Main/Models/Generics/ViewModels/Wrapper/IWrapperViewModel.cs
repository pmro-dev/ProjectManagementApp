namespace Project_Main.Models.Generics.ViewModels.WrapperModels
{
    public interface IWrapperViewModel<InputViewModel, OutputViewModel> where InputViewModel : class, new() where OutputViewModel : class, new()
    {
        InputViewModel InputVM { get; set; }
        OutputViewModel OutputVM { get; set; }
    }
}