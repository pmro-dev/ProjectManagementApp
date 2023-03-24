using TODO_Domain_Entities;

namespace TODO_List_ASPNET_MVC.Models.ViewModels.TodoListViewModels
{
    public class DeleteListViewModel
    {
        public TodoListModel ListModel { get; set; } = new TodoListModel();

        public int TasksCount { get; set; } = 0;
    }
}
