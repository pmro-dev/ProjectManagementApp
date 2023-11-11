using Web.Accounts.Users.Interfaces;

namespace Web.Accounts.Users
{
	public class UserRoleModel : IUserRoleModel
	{
		public UserRoleModel()
		{
			UserId = string.Empty;
			User = new UserModel();
			RoleId = string.Empty;
			Role = new RoleModel();
		}

		public string UserId { get; set; }
		public IUserModel User { get; set; }

		public string RoleId { get; set; }
		public IRoleModel Role { get; set; }
	}
}
