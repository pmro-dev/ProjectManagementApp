using App.Common.Interfaces;
using App.Features.Tasks.Common;
using App.Features.TodoLists.Common.Models;

namespace App.Features.TodoLists.Common.Interfaces;

public interface ITodoListModel : IBasicModelAbstract
{
	ICollection<TaskModel> Tasks { get; set; }
	string UserId { get; set; }

	bool Equals(object? obj);
	int GetHashCode();
	bool IsTheSame(TodoListModel obj);
}