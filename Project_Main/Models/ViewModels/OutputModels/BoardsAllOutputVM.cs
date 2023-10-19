using Project_DomainEntities;

namespace Project_Main.Models.ViewModels.OutputModels
{
    public class BoardsAllOutputVM
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public List<TaskModel> Tasks { get; set; } = new();
    }
}
