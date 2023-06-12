namespace Project_Main.Models.ViewModels.HomeViewModels
{
    /// <summary>
    /// Model for Error View.
    /// </summary>
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}