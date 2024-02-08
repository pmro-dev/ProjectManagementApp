using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Users.Common.Roles.Models.Interfaces;

public interface IUserRoleModel
{
    [Key]
    [Required]
    Guid RoleId { get; set; }

    RoleModel? Role { get; set; }

	[Key]
	[Required]
	string UserId { get; set; }

    UserModel? User { get; set; }
}