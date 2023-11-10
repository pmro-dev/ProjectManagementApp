using static Project_DomainEntities.Helpers.TaskStatusHelper;
using System.ComponentModel.DataAnnotations;
using Project_DomainEntities.Helpers;
using Web.ViewModels.Inputs.Abstract;

namespace Web.ViewModels.Inputs
{
	public class TaskEditInputVM : ITaskEditInputVM
	{
		private const int defaultId = 0;

		public int Id { get; set; }
		public string UserId { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;

		[DataType(DataType.MultilineText)]
		public string Description { get; set; } = string.Empty;

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
		public DateTime DueDate { get; set; } = DateTime.Now;

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
		public DateTime? ReminderDate { get; set; } = null;

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = AttributesHelper.DataFormat, ApplyFormatInEditMode = true)]
		public DateTime LastModificationDate { get; set; } = DateTime.Now;

		public TaskStatusType Status { get; set; } = TaskStatusType.NotStarted;

		public int TodoListId { get; set; } = defaultId;
	}
}
