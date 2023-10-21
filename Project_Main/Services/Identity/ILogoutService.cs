using Microsoft.AspNetCore.Mvc;

namespace Project_Main.Services.Identity
{
    public interface ILogoutService
    {
        Task<IActionResult> LogoutByProviderAsync();
    }
}