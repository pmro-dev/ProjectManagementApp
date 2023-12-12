using App.Features.Pagination;
using App.Features.TodoLists.Common.Models;

namespace App.Features.Boards.Briefly.Interfaces;

public interface IBoardBrieflyOutputVM
{
	List<Tuple<TodoListDto, int, int>> TupleDtos { get; }
	public PaginationData PaginData { get; }
	bool IsReminderForToday(TodoListDto todoList);
}