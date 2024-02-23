namespace App.Features.Projects.Show.ProjectManagerBoards.TeamsBoard;

public class TeamBoardTeamMemberVM : ITeamsBoardTeamMemberVM
{
    public string FullName { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string JobPeriod { get; set; } = string.Empty;
    public int SalaryPerMonth { get; set; }
    public int SalaryPercentageOfTeamBudget { get; set; }
    public int TotalTasksAssignedTodoCount { get; set; }
    public int TasksTodoCount { get; set; }
    public int TasksInProgress { get; set; }
    public int TasksCompleted { get; set; }
    public string BanerColor { get; set; } = string.Empty;
}
