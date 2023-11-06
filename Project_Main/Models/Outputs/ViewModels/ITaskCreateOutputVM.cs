namespace Project_Main.Models.Outputs.ViewModels
{
    public interface ITaskCreateOutputVM
    {
        int TodoListId { get; set; }
        string TodoListName { get; set; }
        string UserId { get; set; }
    }
}