using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Users.Common.Roles.Models.Interfaces;

public interface IRoleModel
{
    [Key]
    [Required]
    Guid Id { get; set; }

	[Required]
	string DataVersion { get; set; }

	string Description { get; set; }

	[Required]
	string Name { get; set; }

    ICollection<UserModel> Users { get; set; }

    ICollection<UserRoleModel> RoleUsers { get; set; }
}