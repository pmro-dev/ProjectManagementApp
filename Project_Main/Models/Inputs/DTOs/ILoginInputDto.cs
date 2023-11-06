namespace Project_Main.Models.Inputs.DTOs
{
    public interface ILoginInputDto
    {
        string Password { get; set; }
        string Username { get; set; }
    }
}