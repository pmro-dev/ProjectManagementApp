using App.Features.TodoLists.Common.Models;

namespace App.Features.Boards.Briefly.Interfaces;

public interface IBoardBrieflyOutputVM
{
	List<Tuple<TodoListDto, int, int>> TupleDtos { get; set; }
	bool IsReminderForToday(TodoListDto todoList);
}