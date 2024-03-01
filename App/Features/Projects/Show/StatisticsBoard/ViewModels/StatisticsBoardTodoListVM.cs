namespace App.Features.Projects.Show.StatisticsBoard.ViewModels;

public class StatisticsBoardTodoListVM : IStatisticsBoardTodoListVM
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Title { get; set; } = string.Empty;
    public string BackgroundColor { get; set; } = string.Empty;
    public string TeamName { get; set; } = string.Empty;
    public string TeamColor { get; set; } = string.Empty;
    public string TeamLiderName { get; set; } = string.Empty;
    public string TeamLiderAvatar { get; set; } = string.Empty;
    public int CompletedTasksCount { get; set; }
    public int TotalTasksCount { get; set; }
    public int ProgressMade { get; set; }
}
