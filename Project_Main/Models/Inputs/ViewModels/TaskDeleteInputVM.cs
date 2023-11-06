namespace Project_Main.Models.Inputs.ViewModels
{
    public class TaskDeleteInputVM : ITaskDeleteInputVM
    {
        public int Id { get; set; }
        public int TodoListId { get; set; }
    }
}
