namespace Project_Main.Services
{
	public interface IRegisterService
	{
		Task<bool> IsPossibleToRegisterUserByProvidedData(string userName);
		Task<bool> RegisterUserAsync(string userName, string userPassword, string userEmail);
	}
}