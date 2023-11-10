namespace Application.DTOs.ViewModels.Inputs.Abstract;

public interface ILoginInputDto
{
    string Password { get; set; }
    string Username { get; set; }
}