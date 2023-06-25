using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project_DomainEntities.Helpers;
using static Project_DomainEntities.Helpers.TaskStatusHelper;

namespace Project_DomainEntities
{
	/// <summary>
	/// Model for Task.
	/// </summary>
	public class TaskModel : BasicModelAbstract
	{
		private const string DataFormat = "{0:dd'/'MM'/'yyyy}";
		private const int defaultId = 0;

        [Key]
		[Required]
		public override int Id { get; set; }

        [Required]
        [MaxLength(AttributesHelper.TitleMaxLength)]
        [MinLength(AttributesHelper.TitleMinLength)]
        public override string Title { get; set; } = string.Empty;

        [Required]
		[DataType(DataType.MultilineText)]
        [MaxLength(AttributesHelper.DescriptionMaxLength)]
        [MinLength(AttributesHelper.DescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
		[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
		public DateTime DueDate { get; set; } = DateTime.Now;

        [Required]
		[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
		public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
		[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
		public DateTime LastModificationDate { get; set; } = DateTime.Now;

		[DisplayFormat(DataFormatString = DataFormat, ApplyFormatInEditMode = true)]
		public DateTime? ReminderDate { get; set; } = null;

		[Required]
        public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

        public List<TaskTagModel> TaskTags { get; set; } = new List<TaskTagModel>();

        [Required]
        public int TodoListId { get; set; } = defaultId;

        [ForeignKey(nameof(TodoListId))]
        public virtual TodoListModel? TodoList { get; set; }

		[Required]
		public string UserId { get; set; } = string.Empty;
	}
}
