using System.ComponentModel.DataAnnotations;
using Web.Common.Helpers;
using Web.Tasks.Common;
using Web.Tasks.Create.Interfaces;

namespace Web.Tasks.Create
{
    public class TaskCreateInputVM : ITaskCreateInputVM
    {
        [Required]
        public int TodoListId { get; set; } = -1;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(TaskAttributesHelper.TitleMaxLength)]
        [MinLength(TaskAttributesHelper.TitleMinLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(TaskAttributesHelper.DescriptionMaxLength)]
        [MinLength(TaskAttributesHelper.DescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
        public DateTime? ReminderDate { get; set; } = null;
    }
}
