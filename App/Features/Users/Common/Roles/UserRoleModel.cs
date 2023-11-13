using App.Features.Users.Interfaces;
using App.Features.Users.Common.Models;

namespace App.Features.Users.Common.Roles;

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
