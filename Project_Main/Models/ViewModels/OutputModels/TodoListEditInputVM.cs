using Project_Main.Models.ViewModels.InputModels;

namespace Project_Main.Models.ViewModels.OutputModels
{
    public class TodoListEditInputVM : ITodoListEditInputVM
    {
        public string Title { get; set; } = string.Empty;
    }
}