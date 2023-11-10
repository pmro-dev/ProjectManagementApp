using Application.DTOs.Entities;

namespace Web.ViewModels.Outputs.Abstract
{
	public interface IBoardBrieflyOutputVM
	{
		ICollection<ITodoListDto> TodoLists { get; set; }

		int GetNumberOfAllTasks(ITodoListDto todoList);
		int GetNumberOfCompletedTasks(ITodoListDto todoList);
		bool IsReminderForToday(ITodoListDto todoList);
	}
}