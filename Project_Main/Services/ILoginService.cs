namespace Project_Main.Services
{
	public interface ILoginService
	{
		Task<bool> CheckThatUserIsRegisteredAsync(string userName, string userPassword);
		Task<bool> LogInUserAsync(string userName, string userPassword);
	}
}