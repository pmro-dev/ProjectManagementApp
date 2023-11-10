using Application.DTOs.ViewModels.Inputs.Abstract;

namespace Application.DTOs.ForViewModels.Inputs;

public class LoginInputDto : ILoginInputDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
