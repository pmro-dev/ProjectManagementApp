using App.Features.TodoLists.Common.Models;

namespace App.Features.Boards.Briefly.Interfaces;

public interface IBoardBrieflyOutputVM
{
    ICollection<TodoListDto> TodoLists { get; set; }

    int GetNumberOfAllTasks(TodoListDto todoList);
    int GetNumberOfCompletedTasks(TodoListDto todoList);
    bool IsReminderForToday(TodoListDto todoList);
}