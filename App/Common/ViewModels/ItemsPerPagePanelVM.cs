namespace App.Common.ViewModels;

public class ItemsPerPagePanelVM
{
	public Guid TodoListId { get; init; }
	public DateTime? FilterDueDate { get; init; }

	public ItemsPerPagePanelVM(Guid todoListId, DateTime? filterDueDate)
	{
		TodoListId = todoListId;
		FilterDueDate = filterDueDate;
	}
}
