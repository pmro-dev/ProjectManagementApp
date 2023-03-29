using Project_IdentityDomainEntities;

namespace Project_Main.Models.DataBases.Repositories
{
	public interface IIdentityRepository : IGenericRepository<UserModel>
	{
		public Task<UserModel> GetWithDetailsAsync(string userId);
		public Task<UserModel> GetForLoggingAsync(string userName, string userPassword);
		public Task<bool> IsNameAlreadyUsedAsync(string userName);
	}
}
