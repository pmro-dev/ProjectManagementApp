using Web.Tasks.Delete.Interfaces;

namespace Web.Tasks.Delete
{
    public class TaskDeleteInputVM : ITaskDeleteInputVM
    {
        public int Id { get; set; }
        public int TodoListId { get; set; }
    }
}
