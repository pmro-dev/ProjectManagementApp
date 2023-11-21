namespace App.Features.Users.Login.Models.Interfaces
{
    public interface ILoginInputVM
    {
        string Password { get; set; }
        string Username { get; set; }
    }
}