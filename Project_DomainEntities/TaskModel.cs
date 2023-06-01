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

		/// <summary>
		/// Compares properties of two Tasks and return result of that compare.
		/// </summary>
		/// <param name="obj">Second Task compare to.</param>
		/// <returns>Result of compare -> true if certain properties (Title, Description) of objects are equal, otherwise false.</returns>
		public override bool Equals(object? obj)
		{
			if (obj == null || !GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				var task = (TaskModel)obj;

				if (Title == task.Title && Description == task.Description)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Create and sets Hash Code.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return HashCode.Combine(Title, Id);
		}
	}
}
