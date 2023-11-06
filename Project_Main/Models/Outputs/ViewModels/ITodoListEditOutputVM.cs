namespace Project_Main.Models.Outputs.ViewModels
{
    public interface ITodoListEditOutputVM
    {
        int Id { get; set; }
        string Title { get; set; }
        string UserId { get; set; }
    }
}