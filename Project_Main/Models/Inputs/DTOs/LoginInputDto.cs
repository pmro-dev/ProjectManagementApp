namespace Project_Main.Models.Inputs.DTOs
{
    public class LoginInputDto : ILoginInputDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
