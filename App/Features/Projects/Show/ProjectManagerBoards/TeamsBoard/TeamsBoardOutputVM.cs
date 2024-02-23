namespace App.Features.Projects.Show.ProjectManagerBoards.TeamsBoard;

public class TeamsBoardOutputVM : ITeamsBoardOutputVM
{
    public ICollection<ITeamsBoardTeamVM> Teams { get; set; } = new List<ITeamsBoardTeamVM>();
    public int TeamsCount { get; set; }
    public string ProjectTitle { get; set; } = string.Empty;
}
