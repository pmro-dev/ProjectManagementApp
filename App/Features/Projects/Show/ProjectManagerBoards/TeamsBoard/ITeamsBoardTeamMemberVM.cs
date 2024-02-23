namespace App.Features.Projects.Show.ProjectManagerBoards.TeamsBoard;

public interface ITeamsBoardTeamMemberVM
{
    string FullName { get; set; }
    string JobTitle { get; set; }
    string Avatar { get; set; }
    string JobPeriod { get; set; }
    int SalaryPerMonth { get; set; }
    int SalaryPercentageOfTeamBudget { get; set; }
    int TotalTasksAssignedTodoCount { get; set; }
    int TasksTodoCount { get; set; }
    int TasksInProgress { get; set; }
    int TasksCompleted { get; set; }
    string BanerColor { get; set; }
}
