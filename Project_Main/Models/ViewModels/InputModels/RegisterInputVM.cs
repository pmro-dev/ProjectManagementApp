using System.ComponentModel.DataAnnotations;

namespace Project_Main.Models.ViewModels.InputModels
{
    /// <summary>
    /// Model for Registration View.
    /// </summary>
    public class RegisterInputVM
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
