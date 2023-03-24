using System.ComponentModel.DataAnnotations;

namespace Project_Main.Models.ViewModels.HomeViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

		[Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
	}
}
