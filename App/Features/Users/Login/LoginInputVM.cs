using System.ComponentModel.DataAnnotations;

namespace App.Features.Users.Login;

/// <summary>
/// Model for Logging View.
/// </summary>
public class LoginInputVM
{
	[Required]
	public string Username { get; set; } = string.Empty;

	[Required]
	public string Password { get; set; } = string.Empty;
}
