namespace Web.Common.ViewModels
{
    public class WrapperViewModel<InputViewModel, OutputViewModel> : IWrapperViewModel<InputViewModel, OutputViewModel> where InputViewModel : class, new() where OutputViewModel : class, new()
    {
        public InputViewModel InputVM { get; set; }
        public OutputViewModel OutputVM { get; set; }

        public WrapperViewModel()
        {
            InputVM = new InputViewModel();
            OutputVM = new OutputViewModel();
        }

        public WrapperViewModel(InputViewModel inputVM, OutputViewModel outputVM)
        {
            InputVM = inputVM;
            OutputVM = outputVM;
        }
    }
}
