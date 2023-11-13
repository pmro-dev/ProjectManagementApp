using App.Features.Users.Login.Interfaces;

namespace App.Features.Users.Login;

public class LoginInputDto : ILoginInputDto
{
	public string Username { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
}
