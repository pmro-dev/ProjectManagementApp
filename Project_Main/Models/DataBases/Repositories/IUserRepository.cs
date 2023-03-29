using Project_IdentityDomainEntities;

namespace Project_Main.Models.DataBases.Repositories
{
	public interface IUserRepository : IGenericRepository<UserModel>
	{
		Task<UserModel> GetWithDetailsAsync(string id);
		Task<UserModel> GetByNameAndPasswordAsync(string name, string password);
		Task<bool> IsNameTakenAsync(string name);
	}
}
