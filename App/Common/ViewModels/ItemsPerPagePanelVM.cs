namespace App.Common.ViewModels;

public class ItemsPerPagePanelVM
{
	public int TodoListId { get; init; }
	public DateTime? FilterDueDate { get; init; }

	public ItemsPerPagePanelVM(int todoListId, DateTime? filterDueDate)
	{
		TodoListId = todoListId;
		FilterDueDate = filterDueDate;
	}
}
