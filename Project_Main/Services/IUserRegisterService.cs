namespace Project_Main.Services
{
	public interface IUserRegisterService
	{
		Task<bool> IsPossibleToRegisterUserByProvidedData(string userName);
		Task<bool> RegisterUserAsync(string userName, string userPassword, string userEmail);
	}
}