using System.ComponentModel.DataAnnotations;

namespace Project_Main.Models.ViewModels.HomeViewModels
{
    /// <summary>
    /// Model for Logging View.
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
