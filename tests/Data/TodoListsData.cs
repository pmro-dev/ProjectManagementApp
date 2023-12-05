using App.Features.TodoLists.Common.Models;

namespace Project_UnitTests.Data;

public static class TodoListsData
{
	public static readonly string AdminId = "adminId";

	public static readonly object[] ValidSimpleTodoLists = new object[]
	{
		new object[] { "App UX"},
		new object[] { "App Backend"},
		new object[] { "App Testing" },
		new object[] { "Project Management" }
	};

	public static readonly object[] InvalidTodoLists = new object[]
	{
		new object[] { "Ap" },
		new object[] { "This is too long Name This is too long Name This is too long Name This is too long Name This is too long Name" },
	};

	public static readonly string ListRangeSuffix = "List Range Test";

	public static List<TodoListModel> NewTodoListsRange { get; private set; } = new()
	{
			new TodoListModel(){ Title = "First " + ListRangeSuffix, },
			new TodoListModel(){ Title = "Second " + ListRangeSuffix, },
			new TodoListModel(){ Title = "Third " + ListRangeSuffix, },
			new TodoListModel(){ Title = "Fourth " + ListRangeSuffix, },
			new TodoListModel(){ Title = "Fifth " + ListRangeSuffix, }
	};
}
