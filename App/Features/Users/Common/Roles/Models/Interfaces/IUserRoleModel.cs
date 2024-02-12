using App.Features.Users.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace App.Features.Users.Common.Roles.Models.Interfaces;

public interface IUserRoleModel
{
	[Timestamp]
	byte[] RowVersion { get; set; }

	[Key]
    [Required]
    Guid RoleId { get; set; }

    RoleModel? Role { get; set; }

	[Key]
	[Required]
	string UserId { get; set; }

    UserModel? User { get; set; }
}