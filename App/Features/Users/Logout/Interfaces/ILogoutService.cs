using Microsoft.AspNetCore.Mvc;

namespace App.Features.Users.Logout.Interfaces;

public interface ILogoutService
{
	Task<IActionResult> LogoutByProviderAsync();
}