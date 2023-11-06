using Project_Main.Models.Inputs.ViewModels;

namespace Project_Main.Models.Outputs.ViewModels
{
    public class TodoListEditInputVM : ITodoListEditInputVM
    {
        public string Title { get; set; } = string.Empty;
    }
}