using System.ComponentModel.DataAnnotations;
using App.Features.Users.Login.Models.Interfaces;

namespace App.Features.Users.Login.Models;

/// <summary>
/// Model for Logging View.
/// </summary>
public class LoginInputVM : ILoginInputVM
{
	[Required]
	public string Username { get; set; } = string.Empty;

	[Required]
	public string Password { get; set; } = string.Empty;
}
