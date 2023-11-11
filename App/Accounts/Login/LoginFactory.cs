using Web.Accounts.Login.Interfaces;

namespace Web.Accounts.Login
{
	public class LoginFactory : ILoginFactory
	{
		public LoginInputDto CreateLoginInputDto()
		{
			return new LoginInputDto();
		}
	}
}
