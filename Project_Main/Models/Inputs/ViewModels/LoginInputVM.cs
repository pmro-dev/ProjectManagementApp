using System.ComponentModel.DataAnnotations;

namespace Project_Main.Models.Inputs.ViewModels
{
    /// <summary>
    /// Model for Logging View.
    /// </summary>
    public class LoginInputVM
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
