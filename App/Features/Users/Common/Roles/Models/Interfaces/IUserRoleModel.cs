using App.Features.Users.Common.Models;

namespace App.Features.Users.Common.Roles.Models.Interfaces;

public interface IUserRoleModel
{
    RoleModel Role { get; set; }
    string RoleId { get; set; }
    UserModel User { get; set; }
    string UserId { get; set; }
}