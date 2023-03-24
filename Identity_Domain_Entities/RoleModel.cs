using System.ComponentModel.DataAnnotations;
using Identity_Domain_Entities.Helpers;

namespace Identity_Domain_Entities
{
    public class RoleModel
	{
		[Key]
		[Required]
		public string Id { get; set; } = Guid.NewGuid().ToString();

		[Required]
		// ConcurrencyStamp
		public string DataVersion { get; set; } = Guid.NewGuid().ToString();

		[Required]
		[MinLength(AttributesHelper.RoleNameMinLength)]
		[MaxLength(AttributesHelper.RoleNameMaxLength)]
		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public List<UserRoleModel> UserRoles { get; set; } = new();
	}
}
