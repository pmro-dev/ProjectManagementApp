using App.Features.Users.Login.Interfaces;
using App.Features.Users.Login.Models;

namespace App.Features.Users.Login;

public class LoginFactory : ILoginFactory
{
	public LoginInputDto CreateLoginInputDto()
	{
		return new LoginInputDto();
	}
}
