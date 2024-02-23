using App.Features.Projects.Show.Interfaces;

namespace App.Features.Projects.Show.Models;

public class ProjectBoardStatisticsOutputVM : IProjectBoardStatisticsOutputVM
{
	public int TodoListsCount { get; set; }
	public int CompletedTasksCount { get; set; }
	public int TotalTasksCount { get; set; }
	public int InProgressTasksCount { get; set; }
	public int TodoTasksCount { get; set; }
	public DateTime ProjectDeadline { get; set; }
	public int DaysTillDeadline { get; set; }
	public string OwnerCompanyName { get; set; }
	public int ProjectBudgetAmount { get; set; }
	public int ProjectBudgetSpent { get; set; }
	public string ProjectTitle { get; set; }

	public ICollection<IStatisticsBoardTodoListVM> TodoLists { get; set; }
	public ICollection<IStatisticsBoardTeamVM> Teams { get; set; }

	public ProjectBoardStatisticsOutputVM(int todoListsCount, int completedTasksCount, int totalTasksCount, 
		int inProgressTasksCount, int todoTasksCount, DateTime projectDeadline, int daysTillDeadline, 
		string ownerCompanyName, int projectBudgetAmount, int projectBudgetSpent, string projectTitle, 
		ICollection<IStatisticsBoardTodoListVM> todoLists, ICollection<IStatisticsBoardTeamVM> teams)
	{
		TodoListsCount = todoListsCount;
		CompletedTasksCount = completedTasksCount;
		TotalTasksCount = totalTasksCount;
		InProgressTasksCount = inProgressTasksCount;
		TodoTasksCount = todoTasksCount;
		ProjectDeadline = projectDeadline;
		DaysTillDeadline = daysTillDeadline;
		OwnerCompanyName = ownerCompanyName;
		ProjectBudgetAmount = projectBudgetAmount;
		ProjectBudgetSpent = projectBudgetSpent;
		ProjectTitle = projectTitle;
		TodoLists = todoLists;
		Teams = teams;
	}
}
