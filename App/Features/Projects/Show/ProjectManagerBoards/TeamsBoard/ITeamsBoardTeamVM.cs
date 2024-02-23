namespace App.Features.Projects.Show.ProjectManagerBoards.TeamsBoard;

public interface ITeamsBoardTeamVM
{
    ICollection<ITeamsBoardTeamMemberVM> Members { get; set; }
    int SalaryBudgetPerMonth { get; set; }
    string Name { get; set; }
    int TasksPerPersonCount { get; set; }
}
