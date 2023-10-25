using System.ComponentModel.DataAnnotations;
using Project_DomainEntities.Helpers;

namespace Project_Main.Models.ViewModels.InputModels
{
    public class TaskCreateInputVM
    {
        [Required]
        public int TodoListId { get; set; } = -1;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(AttributesHelper.TitleMaxLength)]
        [MinLength(AttributesHelper.TitleMinLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(AttributesHelper.DescriptionMaxLength)]
        [MinLength(AttributesHelper.DescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; } = DateTime.Now;
        
        [DataType(DataType.Date)]
        public DateTime? ReminderDate { get; set; } = null;
    }
}
