using System.ComponentModel.DataAnnotations;
using App.Features.Users.Common.Helpers;
using App.Features.Users.Common.Models;
using App.Features.Users.Common.Roles.Models.Interfaces;

namespace App.Features.Users.Common.Roles.Models;

public class RoleModel : IRoleModel
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }

    [Required]
    [MinLength(UserAttributesHelper.RoleNameMinLength)]
    [MaxLength(UserAttributesHelper.RoleNameMaxLength)]
    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<UserModel> Users { get; set; }

    public ICollection<UserRoleModel> UserRoles { get; set; }

    public RoleModel()
    {
        Id = Guid.NewGuid();
        RowVersion = new byte[] { 1, 1, 1 };
        Name = string.Empty;
        Description = string.Empty;
        UserRoles = new List<UserRoleModel>();
        Users = new List<UserModel>();
    }
}
