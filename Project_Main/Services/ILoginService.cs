namespace Project_Main.Services
{
	public interface ILoginService
	{
		Task<bool> IsUserRegisteredAsync(string userName, string userPassword);
		Task<bool> LogInUserAsync();
	}
}