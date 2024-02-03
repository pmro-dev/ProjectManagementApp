namespace App.Common.ViewModels;

public record FilterDueDatePanelModel(
	int ParentId,
	DateTime? FilterDueDate, 
	string ControllerName, 
	string ActionName,
	int PageNumber,
	int ItemsPerPageCount
);