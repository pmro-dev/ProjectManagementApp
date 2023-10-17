namespace Project_Main.Models.ViewModels.OutputModels
{
	public class TaskCreateOutputVM
	{
		public int TodoListId { get; set; }

		public string UserId { get; set; } = string.Empty;

		public string TodoListName { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public DateTime DueDate { get; set; }
		public DateTime ReminderDate { get; set; }
	}
}
