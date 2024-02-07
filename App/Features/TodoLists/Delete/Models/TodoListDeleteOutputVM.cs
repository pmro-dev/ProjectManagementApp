﻿using App.Features.TodoLists.Delete.Interfaces;

namespace App.Features.TodoLists.Delete.Models;

/// <summary>
/// Model for deletion ToDoList.
/// </summary>
public class TodoListDeleteOutputVM : ITodoListDeleteOutputVM
{
    public int TasksCount { get; set; } = 0;
    public Guid Id { get; set; } = Guid.Empty;
    public string Title { get; set; } = string.Empty;
}
