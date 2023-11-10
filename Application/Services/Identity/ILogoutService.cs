using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Identity;

public interface ILogoutService
{
	Task<IActionResult> LogoutByProviderAsync();
}