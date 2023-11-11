namespace Web.Common.ViewModels
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