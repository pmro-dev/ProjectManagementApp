namespace App.Features.Projects.Show.StatisticsBoard.ViewModels;

public class StatisticsBoardTeamVM : IStatisticsBoardTeamVM
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public int MembersCount { get; set; }
    public ICollection<string> MembersAvatars { get; set; } = new List<string>();
    public int TeamCostPerMonth { get; set; }
    public int TasksPerPersonCount { get; set; }
}
