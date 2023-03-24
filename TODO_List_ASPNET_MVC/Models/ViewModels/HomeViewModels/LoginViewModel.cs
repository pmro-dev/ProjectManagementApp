using System.ComponentModel.DataAnnotations;

namespace TODO_List_ASPNET_MVC.Models.ViewModels.HomeViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
