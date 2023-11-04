namespace Project_Main.Models.ViewModels.InputModels
{
    public interface ITaskDeleteInputVM
    {
        int Id { get; set; }
        int TodoListId { get; set; }
    }
}