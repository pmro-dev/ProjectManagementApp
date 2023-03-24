using System.ComponentModel.DataAnnotations;

namespace Project_Main.Models.ViewModels.HomeViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
