namespace Project_Main.Models.Inputs.ViewModels
{
    public interface ITaskDeleteInputVM
    {
        int Id { get; set; }
        int TodoListId { get; set; }
    }
}