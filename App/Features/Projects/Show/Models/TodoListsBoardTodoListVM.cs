﻿using App.Features.Projects.Show.Interfaces;

namespace App.Features.Projects.Show.Models;

public class TodoListsBoardTodoListVM : ITodoListsBoardTodoListVM
{
	public Guid Id { get; set; } = Guid.Empty;
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string TeamName { get; set; } = string.Empty;
	public int CompletedTasksCount { get; set; }
	public int TotalTasksCount { get; set; }
	public int ProgressMade { get; set; }
	public string AvatarImg { get; set; } = string.Empty;
}
