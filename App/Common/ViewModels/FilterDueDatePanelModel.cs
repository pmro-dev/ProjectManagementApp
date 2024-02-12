namespace App.Common.ViewModels;

public record FilterDueDatePanelModel(
	Guid ParentId,
	DateTime? FilterDueDate, 
	string ControllerName, 
	string ActionName,
	int PageNumber,
	int ItemsPerPageCount
);