namespace App.Features.Projects.Show.Interfaces;

public interface ITodoListsBoardTodoListVM
{
	Guid Id { get; set; }
	string Title { get; set; }
	string Description { get; set; }
	string TeamName { get; set; }
	int CompletedTasksCount { get; set; }
	int TotalTasksCount { get; set; }
	int ProgressMade {  get; set; }
	string AvatarImg {  get; set; }
}
