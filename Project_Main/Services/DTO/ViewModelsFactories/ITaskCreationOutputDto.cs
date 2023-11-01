namespace Project_Main.Services.DTO.ViewModelsFactories
{
    public interface ITaskCreationOutputDto
    {
        public int TodoListId { get; set; }
        public string UserId { get; set; }
        public string TodoListName { get; set; }
    }
}