﻿using App.Features.Tasks.Delete.Interfaces;

namespace App.Features.Tasks.Delete.Models;

public class TaskDeleteInputVM : ITaskDeleteInputVM
{
	public int Id { get; set; }
	public int TodoListId { get; set; }
}
