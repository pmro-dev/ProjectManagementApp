using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project_DomainEntities.Helpers;
using static Project_DomainEntities.Helpers.TaskStatusHelper;

namespace Project_DomainEntities
{
	/// <summary>
	/// Model for Task.
	/// </summary>
	public class TaskModel : BasicModelAbstract, ITaskModel
	{
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
		[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
		public DateTime DueDate { get; set; } = DateTime.Now;

        [Required]
		[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
		public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
		[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
		public DateTime LastModificationDate { get; set; } = DateTime.Now;

		[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
		public DateTime? ReminderDate { get; set; } = null;

		[Required]
        public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

        public ICollection<ITaskTagModel> TaskTags { get; set; } = new List<ITaskTagModel>();

        [Required]
        public int TodoListId { get; set; } = defaultId;

        [ForeignKey(nameof(TodoListId))]
        public virtual ITodoListModel? TodoList { get; set; }

		[Required]
		public string UserId { get; set; } = string.Empty;
	}
}
