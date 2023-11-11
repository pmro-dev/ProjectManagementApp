using Web.TodoLists.Edit.Interfaces;

namespace Web.TodoLists.Edit
{
    public class TodoListEditOutputVM : ITodoListEditOutputVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}