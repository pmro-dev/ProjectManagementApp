namespace App.Features.Projects.Show.ProjectManagerBoards.TeamsBoard;

public class TeamsBoardTeamVM : ITeamsBoardTeamVM
{
    public ICollection<ITeamsBoardTeamMemberVM> Members { get; set; } = new List<ITeamsBoardTeamMemberVM>();
    public int SalaryBudgetPerMonth { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TasksPerPersonCount { get; set; }
}
