namespace Project_Main.Infrastructure.DTOs
{
    public interface ILoginInputDto
    {
        string Password { get; set; }
        string Username { get; set; }
    }
}