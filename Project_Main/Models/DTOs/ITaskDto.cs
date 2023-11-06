using static Project_DomainEntities.Helpers.TaskStatusHelper;

namespace Project_Main.Models.DTOs
{
    public interface ITaskDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }

        public DateTime? ReminderDate { get; set; }

        public TaskStatusType Status { get; set; }

        public int TodoListId { get; set; }

        public ITodoListDto? TodoList { get; set; }

        public string UserId { get; set; }

        public ICollection<ITaskTagDto> TaskTags { get; set; }
    }
}