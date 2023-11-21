namespace App.Features.Users.Register.Models.Interfaces
{
    public interface IRegisterInputVM
    {
        string Email { get; set; }
        string Name { get; set; }
        string Password { get; set; }
    }
}