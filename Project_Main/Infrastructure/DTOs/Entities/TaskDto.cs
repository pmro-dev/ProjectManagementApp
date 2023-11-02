using static Project_DomainEntities.Helpers.TaskStatusHelper;
using System.ComponentModel.DataAnnotations;

namespace Project_Main.Infrastructure.DTOs.Entities
{
    public class TaskDto : ITaskDto
    {
        private const string DataFormat = "{0:dd-MM-yyyy}";

        private const int defaultId = 0;

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
        public DateTime LastModificationDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
        public DateTime? ReminderDate { get; set; } = null;

        public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

        public int TodoListId { get; set; } = defaultId;

        public ITodoListDto? TodoList { get; set; }

        public string UserId { get; set; } = string.Empty;

        public IEnumerable<ITaskTagDto> TaskTags { get; set; } = new List<ITaskTagDto>();
    }
}