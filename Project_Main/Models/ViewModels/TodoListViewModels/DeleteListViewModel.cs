using Project_DomainEntities;

namespace Project_Main.Models.ViewModels.TodoListViewModels
{
    /// <summary>
    /// Model for deletion ToDoList.
    /// </summary>
    public class DeleteListViewModel
    {
        public TodoListModel ListModel { get; set; } = new TodoListModel();

        public int TasksCount { get; set; } = 0;
    }
}
