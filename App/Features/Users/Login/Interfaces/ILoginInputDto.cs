namespace App.Features.Users.Login.Interfaces;

public interface ILoginInputDto
{
	string Username { get; set; }
	string Password { get; set; }
}