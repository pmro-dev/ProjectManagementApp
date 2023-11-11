using Web.TodoLists.Edit.Interfaces;

namespace Web.TodoLists.Edit
{
    public class TodoListEditInputVM : ITodoListEditInputVM
    {
        public string Title { get; set; } = string.Empty;
    }
}