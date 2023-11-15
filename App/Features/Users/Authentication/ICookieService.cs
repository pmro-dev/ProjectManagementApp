using Microsoft.AspNetCore.Authentication.Cookies;

namespace App.Features.Users.Authentication
{
    public interface ICookieService
    {
        void SetupOptions(CookieAuthenticationOptions options);
    }
}