using System.ComponentModel.DataAnnotations;

namespace TODO_List_ASPNET_MVC.Models.ViewModels.HomeViewModels
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
