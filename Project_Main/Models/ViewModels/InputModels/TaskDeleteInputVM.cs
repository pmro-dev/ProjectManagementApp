namespace Project_Main.Models.ViewModels.InputModels
{
    public class TaskDeleteInputVM : ITaskDeleteInputVM
    {
        public int Id { get; set; }
        public int TodoListId { get; set; }
    }
}
