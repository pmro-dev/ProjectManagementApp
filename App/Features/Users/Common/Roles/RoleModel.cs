using System.ComponentModel.DataAnnotations;

namespace App.Features.Users.Common.Roles;

public class RoleModel : IRoleModel
{
	[Key]
	[Required]
	public string Id { get; set; }

	[Required]
	// ConcurrencyStamp
	public string DataVersion { get; set; }

	[Required]
	[MinLength(UserAttributesHelper.RoleNameMinLength)]
	[MaxLength(UserAttributesHelper.RoleNameMaxLength)]
	public string Name { get; set; }

	public string Description { get; set; }

	public ICollection<UserRoleModel> UserRoles { get; set; }

	public RoleModel()
	{
		Id = Guid.NewGuid().ToString();
		DataVersion = Guid.NewGuid().ToString();
		Name = string.Empty;
		Description = string.Empty;
		UserRoles = new List<UserRoleModel>();
	}
}
