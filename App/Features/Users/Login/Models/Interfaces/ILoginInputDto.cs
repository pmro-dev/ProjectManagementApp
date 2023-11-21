namespace App.Features.Users.Login.Models.Interfaces;

public interface ILoginInputDto
{
    string Username { get; set; }
    string Password { get; set; }
}