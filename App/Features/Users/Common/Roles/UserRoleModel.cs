using App.Features.Users.Common.Models;
using App.Features.Users.Interfaces;

namespace App.Features.Users.Common.Roles;

public class UserRoleModel : IUserRoleModel
{
    public UserRoleModel()
    {
        User = new UserModel();
        Role = new RoleModel();
    }

    public UserRoleModel(IUserModel userModel, IRoleModel roleModel)
    {
        User = userModel;
        Role = roleModel;
    }

    public string UserId { get; set; } = string.Empty;
	public IUserModel User { get; set; }

    public string RoleId { get; set; } = string.Empty;
	public IRoleModel Role { get; set; }
}
