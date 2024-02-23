namespace App.Features.Projects.Show.ProjectManagerBoards.TeamsBoard;

public interface ITeamsBoardOutputVM
{
    ICollection<ITeamsBoardTeamVM> Teams { get; set; }
    int TeamsCount { get; set; }
    string ProjectTitle { get; set; }
}
