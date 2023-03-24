namespace Identity_Domain_Entities
{
	public class UserRoleModel
	{
		public string UserId { get; set; } = string.Empty;
		public UserModel User { get; set; } = new();

		public string RoleId { get; set; } = string.Empty;
		public RoleModel Role { get; set; } = new();

	}
}
