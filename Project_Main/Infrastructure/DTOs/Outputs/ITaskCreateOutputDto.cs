namespace Project_Main.Infrastructure.DTOs.Outputs
{
    public interface ITaskCreateOutputDto
    {
        public int TodoListId { get; set; }
        public string UserId { get; set; }
        public string TodoListName { get; set; }
    }
}