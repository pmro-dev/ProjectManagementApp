using Project_Main.Models.DTOs;

namespace Project_Main.Models.Outputs.ViewModels
{
    public interface IBoardBrieflyOutputVM
    {
        ICollection<ITodoListDto> TodoLists { get; set; }

        int GetNumberOfAllTasks(ITodoListDto todoList);
        int GetNumberOfCompletedTasks(ITodoListDto todoList);
        bool IsReminderForToday(ITodoListDto todoList);
    }
}