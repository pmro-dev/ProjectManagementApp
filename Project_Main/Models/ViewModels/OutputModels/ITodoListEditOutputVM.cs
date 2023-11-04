namespace Project_Main.Models.ViewModels.OutputModels
{
    public interface ITodoListEditOutputVM
    {
        int Id { get; set; }
        string Title { get; set; }
        string UserId { get; set; }
    }
}