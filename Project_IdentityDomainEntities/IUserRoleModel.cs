namespace Project_IdentityDomainEntities
{
	public interface IUserRoleModel
	{
		IRoleModel Role { get; set; }
		string RoleId { get; set; }
		IUserModel User { get; set; }
		string UserId { get; set; }
	}
}