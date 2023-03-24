using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Project_DomainEntities;
using Project_DomainEntities.Helpers;
using static Project_DomainEntities.Helpers.TaskStatusHelper;

namespace Project_Main.Models.ViewModels.TaskViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(AttributesHelper.TitleMaxLength)]
        [MinLength(AttributesHelper.TitleMinLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(AttributesHelper.DescriptionMaxLength)]
        [MinLength(AttributesHelper.DescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime DueDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime LastModificationDate { get; set; } = DateTime.Now;

        public DateTime? ReminderDate { get; set; } = null;

        [Required]
        public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

        public List<TagModel> Tags { get; set; } = new List<TagModel>();

        public SelectList? StatusSelector { get; set; }

        public string TodoListName { get; set; } = string.Empty;

        [Required]
        public int TodoListId { get; set; } = -1;

        [Required]
        public string UserId { get; set; } = string.Empty;

        public SelectList? TodoListsSelector { get; set; }
    }
}
