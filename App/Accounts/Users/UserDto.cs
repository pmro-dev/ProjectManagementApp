using Web.Accounts.Users.Interfaces;

namespace Web.Accounts.Users
{
	public class UserDto : IUserDto
	{
		public string UserId { get; set; } = string.Empty;
		public string DataVersion { get; set; } = string.Empty;
		public string Provider { get; set; } = string.Empty;
		public string NameIdentifier { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public ICollection<IUserRoleModel> UserRoles { get; set; } = new List<IUserRoleModel>();
	}
}
