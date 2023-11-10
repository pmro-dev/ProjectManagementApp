using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Inputs
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
