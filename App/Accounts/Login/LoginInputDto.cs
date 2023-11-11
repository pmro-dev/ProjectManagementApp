using Web.Accounts.Login.Interfaces;

namespace Web.Accounts.Login;

public class LoginInputDto : ILoginInputDto
{
	public string Username { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
}
