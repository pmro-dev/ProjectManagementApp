namespace Project_Main.Models.ViewModels.OutputModels
{
    /// <summary>
    /// Model for Error View.
    /// </summary>
    public class ErrorOutputVM
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}