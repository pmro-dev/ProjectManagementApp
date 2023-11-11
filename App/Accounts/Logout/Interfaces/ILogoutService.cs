using Microsoft.AspNetCore.Mvc;

namespace Web.Accounts.Logout.Interfaces;

public interface ILogoutService
{
    Task<IActionResult> LogoutByProviderAsync();
}