using System.ComponentModel.DataAnnotations;
using Web.Common.Helpers;
using Web.Tasks.Show.Interfaces;
using static Web.Tasks.Common.TaskStatusHelper;

namespace Web.Tasks.Show
{
    public class TaskDetailsOutputVM : ITaskDetailsOutputVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
        public DateTime LastModificationDate { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
        public DateTime? ReminderDate { get; set; } = null;

        public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

        public int TodoListId { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}
