using Microsoft.AspNetCore.Authentication.Cookies;

namespace App.Features.Users.Authentication.Interfaces
{
    public interface ICookieService
    {
        void SetupOptions(CookieAuthenticationOptions options);
    }
}