using System.ComponentModel.DataAnnotations;

namespace Project_Main.Infrastructure.DTOs
{
    public class TaskCreateInputVMDto
    {
        private const string DataFormat = "{0:dd'/'MM'/'yyyy}";

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DueDate { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
        public DateTime? ReminderDate { get; set; } = null;

        public string UserId { get; set; } = string.Empty;

        public int TodoListId { get; set; }
    }
}
