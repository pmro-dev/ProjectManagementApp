using Identity_Domain_Entities;

namespace TODO_List_ASPNET_MVC.Models.DataBases.Repositories
{
	public interface IIdentityRepository
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
