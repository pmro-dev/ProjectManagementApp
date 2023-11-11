using System.ComponentModel.DataAnnotations;
using Web.Accounts.Common;
using Web.Accounts.Users.Interfaces;

namespace Web.Accounts.Users
{
	public class RoleModel : IRoleModel
	{
		[Key]
		[Required]
		public string Id { get; set; }

		[Required]
		// ConcurrencyStamp
		public string DataVersion { get; set; }

		[Required]
		[MinLength(AccountAttributesHelper.RoleNameMinLength)]
		[MaxLength(AccountAttributesHelper.RoleNameMaxLength)]
		public string Name { get; set; }

		public string Description { get; set; }

		public ICollection<IUserRoleModel> UserRoles { get; set; }

		public RoleModel()
		{
			Id = Guid.NewGuid().ToString();
			DataVersion = Guid.NewGuid().ToString();
			Name = string.Empty;
			Description = string.Empty;
			UserRoles = new List<IUserRoleModel>();
		}
	}
}
