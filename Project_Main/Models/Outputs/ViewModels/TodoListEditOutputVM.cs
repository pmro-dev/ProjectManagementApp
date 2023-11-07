﻿namespace Project_Main.Models.Outputs.ViewModels
{
    public class TodoListEditOutputVM : ITodoListEditOutputVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}