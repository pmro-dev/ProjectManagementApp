namespace Project_Main.Models.Inputs.ViewModels
{
    public class TodoListEditInputVM : ITodoListEditInputVM
    {
        public string Title { get; set; } = string.Empty;
    }
}