namespace Project_IdentityDomainEntities
{
	public interface IRoleModel
	{
		string DataVersion { get; set; }
		string Description { get; set; }
		string Id { get; set; }
		string Name { get; set; }
		ICollection<IUserRoleModel> UserRoles { get; set; }
	}
}