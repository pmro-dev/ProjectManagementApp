namespace App.Features.Users.Login.Interfaces;

public interface ILoginInputDto
{
	string Password { get; set; }
	string Username { get; set; }
}