namespace App.Features.Projects.Show.Interfaces;

public interface IStatisticsBoardTeamVM
{
	Guid Id { get; set; }
	string Name { get; set; }
	int MembersCount { get; set; }
	ICollection<string> MembersAvatars { get; set; }
	int TeamCostPerMonth { get; set; }
	int TasksPerPersonCount { get; set; }
}
