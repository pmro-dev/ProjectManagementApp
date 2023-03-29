using Project_IdentityDomainEntities;

namespace Project_Main.Models.DataBases.Repositories
{
	public interface IIdentityRepository : IGenericRepository<UserModel>
	{
		public Task<bool> AddUserAsync(UserModel newUser);
		public Task<UserModel> GetUserWithDetailsAsync(string userId);
		public Task<UserModel> GetUserAsync(string userId);
		public Task<UserModel> GetUserForLoggingAsync(string userName, string userPassword);
		public Task<bool> UpdateUserAsync(UserModel userToUpdate);
		public Task<bool> DeleteUserAsync(string userId);
		public Task<bool> IsUserNameUsedAsync(string userName);
	}
}
