using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Features.Users.Common.Roles.Models;

public class UserRoleModel : IUserRoleModel
{
    [Timestamp]
    public byte[] RowVersion { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
    [ForeignKey(nameof(UserId))]
    public UserModel? User { get; set; }

    [Required]
    public Guid RoleId { get; set; } = Guid.Empty;
    [ForeignKey(nameof(RoleId))]
    public RoleModel? Role { get; set; }

    public UserRoleModel()
    {
        User = new UserModel();
        Role = new RoleModel();
        RowVersion = new byte[] { 1, 1, 1 };
    }

    public UserRoleModel(UserModel userModel, RoleModel roleModel)
    {
        User = userModel;
        Role = roleModel;
        RowVersion = new byte[] { 1, 1, 1 };
    }
}
