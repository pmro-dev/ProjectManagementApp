using Microsoft.AspNetCore.Authentication.Cookies;

namespace App.Features.Users.Authentication.Interfaces
{
    public interface ICookieEventsService
    {
        Task OnSigningInManageUserIdentityAsync(CookieSigningInContext cookieSigningInContext);
    }
}