using Microsoft.AspNetCore.Mvc;

namespace Project_Main.Services
{
	public interface ILogoutService
	{
		Task<ActionResult> LogoutByProviderTypeAsync(ControllerBase controller, string userAuthenticationScheme);
	}
}