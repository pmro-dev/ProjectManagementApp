﻿namespace Project_Main.Models.ViewModels.InputModels
{
    public class TodoListCreateInputVM : ITodoListCreateInputVM
    {
        public string UserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}