using System.ComponentModel.DataAnnotations;

namespace TODO_List_ASPNET_MVC.Models.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		public string? Name { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }
	}
}
