namespace App.Features.Projects.Show.Interfaces;

public interface IStatisticsBoardTodoListVM
{
	Guid Id { get; set; }
	string Title { get; set; }
	string BackgroundColor { get; set; }
	string TeamName { get; set; }
	string TeamColor { get; set; }
	string TeamLiderName { get; set; }
	string TeamLiderAvatar { get; set; }
	int CompletedTasksCount { get; set; }
	int TotalTasksCount { get; set; }
	int ProgressMade { get; set; }
}
