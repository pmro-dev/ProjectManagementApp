using App.Features.Users.Login.Interfaces;

namespace App.Features.Users.Login;

public class LoginFactory : ILoginFactory
{
	public LoginInputDto CreateLoginInputDto()
	{
		return new LoginInputDto();
	}
}
