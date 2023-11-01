namespace Project_Main.Models.ViewModels.OutputModels
{
    public interface ITaskCreationOutputVM : ITaskViewModel
    {
        int TodoListId { get; set; }
        string TodoListName { get; set; }
        string UserId { get; set; }
    }
}