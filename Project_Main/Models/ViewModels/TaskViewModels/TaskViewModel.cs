using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Project_DomainEntities;
using Project_DomainEntities.Helpers;
using static Project_DomainEntities.Helpers.TaskStatusHelper;

namespace Project_Main.Models.ViewModels.TaskViewModels
{
    /// <summary>
    /// Model for a Task creation and editing view.
    /// </summary>
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

        /// <summary>
        /// Deadline for a task accomplishment.
        /// </summary>
        [Required]
        public DateTime DueDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Date of task creation.
        /// </summary>
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Date of last task data modification.
        /// </summary>
        [Required]
        public DateTime LastModificationDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Date when reminder for a specific task should occur.
        /// </summary>
        public DateTime? ReminderDate { get; set; } = null;

        [Required]
        public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

        public List<TaskTagModel> TaskTags { get; set; } = new List<TaskTagModel>();

		[Required]
		public int TodoListId { get; set; } = -1;

		[Required]
		public string UserId { get; set; } = string.Empty;

		public string TodoListName { get; set; } = string.Empty;

		/// <summary>
		/// Task's statuses to pick.
		/// </summary>
		public SelectList? StatusSelector { get; set; }

        /// <summary>
        /// ToDoLists to pick as owner.
        /// </summary>
        public SelectList? TodoListsSelector { get; set; }
    }
}
