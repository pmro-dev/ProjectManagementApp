using Project_DomainEntities;

namespace Project_Main.Models.ViewModels.TodoListViewModels
{
    public class DeleteListViewModel
    {
        public TodoListModel ListModel { get; set; } = new TodoListModel();

        public int TasksCount { get; set; } = 0;
    }
}
