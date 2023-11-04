namespace Project_Main.Infrastructure.DTOs.Inputs
{
    internal class TodoListEditInputDto : ITodoListEditInputDto
    {
        public string Title { get; set; } = string.Empty;
    }
}