namespace App.Features.Projects.Show.StatisticsBoard.ViewModels;

public interface IProjectBoardStatisticsOutputVM
{
    int TodoListsCount { get; set; }
    int CompletedTasksCount { get; set; }
    int TotalTasksCount { get; set; }
    int InProgressTasksCount { get; set; }
    int TodoTasksCount { get; set; }
    DateTime ProjectDeadline { get; set; }
    int DaysTillDeadline { get; set; }
    string OwnerCompanyName { get; set; }
    int ProjectBudgetAmount { get; set; }
    int ProjectBudgetSpent { get; set; }
    string ProjectTitle { get; set; }

    ICollection<IStatisticsBoardTodoListVM> TodoLists { get; set; }
    ICollection<IStatisticsBoardTeamVM> Teams { get; set; }
}
