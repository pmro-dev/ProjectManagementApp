namespace Project_Main.Models.Inputs.DTOs
{
    public class TodoListEditInputDto : ITodoListEditInputDto
    {
        public string Title { get; set; } = string.Empty;
    }
}