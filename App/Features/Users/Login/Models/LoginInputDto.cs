using App.Features.Users.Login.Models.Interfaces;

namespace App.Features.Users.Login.Models;

public class LoginInputDto : ILoginInputDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
