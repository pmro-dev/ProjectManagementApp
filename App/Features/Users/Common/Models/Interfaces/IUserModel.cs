using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Models;

namespace App.Features.Users.Common.Models.Interfaces;

public interface IUserModel : IEquatable<UserModel>
{
    string DataVersion { get; set; }
    string Email { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string NameIdentifier { get; set; }
    string Password { get; set; }
    string Provider { get; set; }
    string UserId { get; set; }
    string Username { get; set; }
    ICollection<UserRoleModel> UserRoles { get; set; }
}