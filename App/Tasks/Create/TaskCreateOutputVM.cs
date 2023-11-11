using Web.Tasks.Create.Interfaces;

namespace Web.Tasks.Create
{
    public class TaskCreateOutputVM : ITaskCreateOutputVM
    {
        public int TodoListId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string TodoListName { get; set; } = string.Empty;
    }
}
