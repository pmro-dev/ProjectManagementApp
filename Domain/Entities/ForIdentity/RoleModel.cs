using System.ComponentModel.DataAnnotations;
using Domain.Interfaces.ForIdentity;
using Project_IdentityDomainEntities.Helpers;

namespace Project_IdentityDomainEntities
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
		[MinLength(AttributesHelper.RoleNameMinLength)]
		[MaxLength(AttributesHelper.RoleNameMaxLength)]
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
