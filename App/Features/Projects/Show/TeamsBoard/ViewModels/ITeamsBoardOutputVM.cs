namespace App.Features.Projects.Show.TeamsBoard.ViewModels;

public interface ITeamsBoardOutputVM
{
    ICollection<ITeamsBoardTeamVM> Teams { get; set; }
    int TeamsCount { get; set; }
    string ProjectTitle { get; set; }
}
