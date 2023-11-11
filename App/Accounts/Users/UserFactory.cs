using Web.Accounts.Users.Interfaces;

namespace Web.Accounts.Users
{
	public class UserFactory : IUserFactory
	{
		public UserModel CreateModel()
		{
			return new UserModel();
		}

		public UserDto CreateDto()
		{
			return new UserDto();
		}
	}
}
