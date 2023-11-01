namespace Project_Main.Infrastructure.DTOs
{
    public class LoginInputDto : ILoginInputDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
