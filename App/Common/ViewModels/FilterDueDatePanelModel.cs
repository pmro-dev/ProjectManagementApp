namespace App.Common.ViewModels;

public record FilterDueDatePanelModel(
	int? OwnerId, 
	DateTime? FilterDueDate, 
	string ControllerName, 
	string ActionName
){}
