namespace Web.Accounts.Login.Interfaces;

public interface ILoginInputDto
{
    string Password { get; set; }
    string Username { get; set; }
}