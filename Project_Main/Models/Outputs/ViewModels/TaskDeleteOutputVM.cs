using static Project_DomainEntities.Helpers.TaskStatusHelper;
using System.ComponentModel.DataAnnotations;

namespace Project_Main.Models.Outputs.ViewModels
{
    public class TaskDeleteOutputVM : ITaskDeleteOutputVM
    {
        private const string DataFormat = "{0:dd'/'MM'/'yyyy}";

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
        public DateTime LastModificationDate { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
        public DateTime? ReminderDate { get; set; } = null;

        public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

        public int TodoListId { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}
