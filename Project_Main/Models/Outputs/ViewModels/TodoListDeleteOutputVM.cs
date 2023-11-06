namespace Project_Main.Models.Outputs.ViewModels
{
    /// <summary>
    /// Model for deletion ToDoList.
    /// </summary>
    public class TodoListDeleteOutputVM : ITodoListDeleteOutputVM
    {
        public int TasksCount { get; set; } = 0;
        public int Id { get; set; } = 0;
        public string Title { get; set; } = string.Empty;
    }
}
