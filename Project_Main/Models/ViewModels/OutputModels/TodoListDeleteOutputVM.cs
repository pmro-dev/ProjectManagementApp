using Project_DomainEntities;

namespace Project_Main.Models.ViewModels.OutputModels
{
    /// <summary>
    /// Model for deletion ToDoList.
    /// </summary>
    public class TodoListDeleteOutputVM
    {
        public TodoListModel ListModel { get; set; } = new TodoListModel();

        public int TasksCount { get; set; } = 0;
    }
}
