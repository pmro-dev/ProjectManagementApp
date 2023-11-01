using Project_Main.Infrastructure.DTOs;

namespace Project_Main.Models.ViewModels.OutputModels
{
    public interface IBoardsBrieflyOutputVM
    {
        IEnumerable<ITodoListDto> TodoLists { get; set; }

        int GetNumberOfAllTasks(ITodoListDto todoList);
        int GetNumberOfCompletedTasks(ITodoListDto todoList);
        bool IsReminderForToday(ITodoListDto todoList);
    }
}