using Web.TodoLists.Common.Interfaces;

namespace Web.Boards.Interfaces
{
    public interface IBoardBrieflyOutputVM
    {
        ICollection<ITodoListDto> TodoLists { get; set; }

        int GetNumberOfAllTasks(ITodoListDto todoList);
        int GetNumberOfCompletedTasks(ITodoListDto todoList);
        bool IsReminderForToday(ITodoListDto todoList);
    }
}