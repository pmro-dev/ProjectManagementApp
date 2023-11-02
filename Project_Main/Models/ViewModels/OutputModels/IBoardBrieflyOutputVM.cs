using Project_Main.Infrastructure.DTOs.Entities;

namespace Project_Main.Models.ViewModels.OutputModels
{
    public interface IBoardBrieflyOutputVM : IBoardViewModel
    {
        IEnumerable<ITodoListDto> TodoLists { get; set; }

        int GetNumberOfAllTasks(ITodoListDto todoList);
        int GetNumberOfCompletedTasks(ITodoListDto todoList);
        bool IsReminderForToday(ITodoListDto todoList);
    }
}